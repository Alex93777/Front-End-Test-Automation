const { test, describe, beforeEach, afterEach, beforeAll, afterAll, expect } = require('@playwright/test');
const { chromium } = require('playwright');

const host = 'http://localhost:3000'; // Application host (NOT service host - that can be anything)

let browser;
let context;
let page;

let user = {
    email: "",
    password: "123456",
    confirmPass: "123456",
};

let pet = {
    age: "2 years",
    name: "",
    breed: "Random breed",
    image: "",
    weight: "2 kg"
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
        test("register makes correct API call", async () => {
            //arrange
            await page.goto(host);
            await page.click('text=Register');
            await page.waitForSelector('form');

            let random = Math.floor(Math.random() * 10000);
            user.email = `email${random}@abv.bg`;

            //act
            await page.locator("#email").fill(user.email);
            await page.locator("#password").fill(user.password);
            await page.locator("#repeatPassword").fill(user.confirmPass);
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
            expect(response.ok()).toBeTruthy();
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
        test("logged in user should see correct buttons", async () => {
            //arrange
            await page.goto(host);

            //act
            await page.click('text=Login');
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');

            //assert
            await expect(page.locator('nav >> text=Home')).toBeVisible();
            await expect(page.locator('nav >> text=Dashboard')).toBeVisible();
            await expect(page.locator('nav >> text=Create Postcard')).toBeVisible();
            await expect(page.locator('nav >> text=Logout')).toBeVisible();

            await expect(page.locator('nav >> text=Login')).toBeHidden();
            await expect(page.locator('nav >> text=Register')).toBeHidden();
        })

        test("guesed user should see correct buttons", async() => {
            //act
            await page.goto(host);

            //assert
            await expect(page.locator('nav >> text=Home')).toBeVisible();
            await expect(page.locator('nav >> text=Dashboard')).toBeVisible();
            await expect(page.locator('nav >> text=Login')).toBeVisible();
            await expect(page.locator('nav >> text=Register')).toBeVisible();
            
            await expect(page.locator('nav >> text=Create Postcard')).toBeHidden();
            await expect(page.locator('nav >> text=Logout')).toBeHidden();
        })
    });

    describe("CRUD", () => {
        beforeEach(async() => {                                         
            await page.goto(host);
            await page.click('text=Login');
            await page.waitForSelector('form');
            await page.locator('#email').fill(user.email);
            await page.locator('#password').fill(user.password);
            await page.click('[type="submit"]');
        })

        test("create makes correct API call", async () => {
            //arrange
            await page.click('text=Create Postcard');
            await page.waitForSelector('form');

            //act
            await page.fill('[name="name"]', "Random Name");
            await page.fill('[name="breed"]', "Random Breed");
            await page.fill('[name="age"]', "7");
            await page.fill('[name="weight"]', "25");
            await page.fill('[name="image"]', "/images/cat-create.jpg");

            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/pets") && response.status() == 200),
                page.click('[type="submit"]')
            ]);
            let eventData = await response.json();

            //assert
            expect(response.ok()).toBeTruthy();
            expect(eventData.name).toEqual("Random Name");
            expect(eventData.age).toEqual("7");
            expect(eventData.breed).toEqual("Random Breed");
            expect(eventData.weight).toEqual("25");
            expect(eventData.image).toEqual("/images/cat-create.jpg");
        })

        test("edit makes correct API call", async () =>{
            //arrange
            await page.click('text=Dashboard');
            await page.locator('text=Details').first().click();
            await page.click('text=Edit');
            await page.waitForSelector('form');

            //act
            await page.fill('[name="name"]', "Random Edited Name");
            await page.fill('[name="age"]', "8");
            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/pets") && response.status() == 200),
                page.click('[type="submit"]')
            ]);
            let eventData = await response.json();

            //assert
            expect(response.ok()).toBeTruthy();
            expect(eventData.name).toEqual("Random Edited Name");
            expect(eventData.age).toEqual("8");
            expect(eventData.breed).toEqual("Random Breed");
            expect(eventData.weight).toEqual("25");
            expect(eventData.image).toEqual("/images/cat-create.jpg");
        })

        test("delete makes correct API call", async () =>{
            //act
            await page.click('text=Dashboard');
            await page.locator('text=Details').first().click();

            let [response] = await Promise.all([
                page.waitForResponse(response => response.url().includes("/data/pets") && response.status() == 200),
                page.on('dialog', dialog => dialog.accept()),
                page.click('text=Delete')
            ]); 

            //assert
            expect(response.ok()).toBeTruthy();
        })
    })
})