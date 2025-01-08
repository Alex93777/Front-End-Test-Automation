const { test, describe, beforeEach, afterEach, beforeAll, afterAll, expect } = require('@playwright/test');
const { chromium } = require('playwright');

const host = 'http://localhost:3000'; // Application host (NOT service host - that can be anything)

let browser;
let context;
let page;

let user = {
    email : "",
    password : "123456",
    confirmPass : "123456",
};

describe("e2e tests", () => {
    beforeAll(async () => {
        browser = await chromium.launch();
    });

    afterAll(async () => {
        await browser.close();
    });

    beforeEach(async () => {
        context = await browser.newContext();
        page = await context.newPage();
    });

    afterEach(async () => {
        await page.close();
        await context.close();
    });

    
    describe("authentication", () => {
        test("register makes correct API call", async () =>{
            //arrange
            await page.goto(host);
            await page.click('text=Register');
            await page.waitForSelector('form');

            let random = Math.floor(Math.random() * 10000);
            user.email = `email${random}@abv.bg`;

            //act
            await page.locator("#email").fill(user.email);
            await page.locator("#password").fill(user.password);
            await page.locator("#repeat-pass").fill(user.confirmPass);
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/users/register") && response.status() == 200),
                page.click('[type="submit"]')
            ]);
            let userData = await response.json();

            //assert
            expect(response.ok()).toBeTruthy();
            expect(userData.email).toBe(user.email);
            expect(userData.password).toBe(user.password);
        })

        test("login makes correct API call", async () => {
            //arrange
            await page.goto(host);
            await page.click('text=Login');
            await page.waitForSelector('form');

            //act
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);

            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/users/login") && response.status() == 200),
                page.click('[type="submit"]')
            ]);
            let userData = await response.json();

            //assert
            await expect(response.ok()).toBeTruthy();
            expect(userData.email).toBe(user.email);
            expect(userData.password).toBe(user.password);
        })

        test("logout makes correct API call", async () => {
            //arrange
            await page.goto(host);
            await page.click('text=Login');
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');

            //act
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/users/logout") && response.status() == 204),
                page.locator('nav >> text=Logout').click()
            ]);

            //assert
            expect(response.ok()).toBeTruthy();
            await page.waitForSelector('text=Login');
            expect(page.url()).toBe(host + '/');
        })
    })

    describe("navbar", () => {
        test("logged in user should see correct buttons", async() => {
            //arrange
            await page.goto(host);

            //act
            await page.click('text=Login');
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');

            //assert
            await expect(page.locator('nav >> text=Dashboard')).toBeVisible();
            await expect(page.locator('nav >> text=My Books')).toBeVisible();
            await expect(page.locator('nav >> text=Add Book')).toBeVisible();
            await expect(page.locator('nav >> text=Logout')).toBeVisible();

            await expect(page.locator('nav >> text=Login')).toBeHidden();
            await expect(page.locator('nav >> text=Register')).toBeHidden();

        })

        test("guess user should see correct buttons", async() => {
            //act
            await page.goto(host);
       
            //assert
            await expect(page.locator('nav >> text=Dashboard')).toBeVisible();
            await expect(page.locator('nav >> text=My Books')).toBeHidden();
            await expect(page.locator('nav >> text=Add Book')).toBeHidden();
            await expect(page.locator('nav >> text=Logout')).toBeHidden();

            await expect(page.locator('nav >> text=Login')).toBeVisible();
            await expect(page.locator('nav >> text=Register')).toBeVisible();
        })
    });

    describe("CRUD", () => {
        beforeEach(async() => {                                         //преди всеки тест да логва user
            await page.goto(host);
            await page.click('text=Login');
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');
        })

        test("create makes correct API call", async () => {
            //arrange
            await page.click('text=Add Book');
            await page.waitForSelector('form');

            //act
            await page.fill('[name="title"]', "Random Title");
            await page.fill('[name="description"]', "Random Description");
            await page.fill('[name="imageUrl"]', "/images/book.png");
            await page.locator('#type').selectOption("Other");

            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/books") && response.status() == 200),
                page.click('[type="submit"]')
            ]);
            let eventData = await response.json();

            //assert
            expect(response.ok()).toBeTruthy();
            expect(eventData.title).toEqual("Random Title");
            expect(eventData.description).toEqual("Random Description");
            expect(eventData.imageUrl).toEqual("/images/book.png");
            expect(eventData.type).toEqual("Other");
        })

        test("edit makes correct API call", async () =>{
            //arrange
            await page.click('text=My Books');
            await page.locator('text=Details').first().click();
            await page.click('text=Edit');
            await page.waitForSelector('form');

            //act
            await page.fill('[name="title"]', "Random Edited Title");
            await page.locator('#type').selectOption("Other");
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/books") && response.status() == 200),
                page.click('[type="submit"]')
            ]);
            let eventData = await response.json();

            //assert
            expect(response.ok()).toBeTruthy();
            expect(eventData.title).toEqual("Random Edited Title");
            expect(eventData.description).toEqual("Random Description");
            expect(eventData.imageUrl).toEqual("/images/book.png");
            expect(eventData.type).toEqual("Other");
        })

        test("delete makes correct API call", async () =>{
            //act
            await page.click('text=My Books');
            await page.locator('text=Details').first().click();
            
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/books") && response.status() == 200),
                page.click('text=Delete')
            ]); 

            //assert
            expect(response.ok()).toBeTruthy();
        })
    })
})