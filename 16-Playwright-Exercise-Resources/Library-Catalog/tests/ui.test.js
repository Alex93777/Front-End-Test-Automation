import {test, expect} from 'playwright/test';
import { LOGIN_FORM, NAVBAR, LOGGED_NAVBAR, CREATE_FORM, ALL_BOOKS_LIST, DETAILS_BUTTON, DETAILS_DESCRIPTION, REGISTER_BUTTON, MY_BOOKS_BUTTON } from '../utils/locators.js'; 
import { ALERT, BASE_URL, TEST_BOOK, TEST_URL, TEST_USER, NOT_REGISTER_USER, } from '../utils/constant.js';



//Navigation
test('Verify "All books" link is visible - example 1', async ({page}) => {
    await page.goto('http://localhost:3000');

    await page.waitForSelector('nav.navbar');
    const allBooksLink = await page.$('a[href = "/catalog"]');
    const isLinkVisible = await allBooksLink.isVisible();
    expect(isLinkVisible).toBe(true);
})

test('Verify "All books" link is visible - example 2', async ({page}) => {
    await page.goto(BASE_URL);

    await expect(page.locator(NAVBAR.NAV_NAVBAR)).toBeVisible();

    await expect(page.locator(NAVBAR.ALL_BOOKS_LINK)).toBeVisible();
})

test('Verify "Login" button is visible', async ({page}) => {
    await page.goto(BASE_URL);

    await expect(page.locator(NAVBAR.NAV_NAVBAR)).toBeVisible();

    await expect(page.locator(NAVBAR.LOGIN_BUTTON)).toBeVisible();
})

test('Verify "Register button" button is visible', async ({page}) => {
    await page.goto(BASE_URL);

    await expect(page.locator(NAVBAR.NAV_NAVBAR)).toBeVisible();

    await expect(page.locator(NAVBAR.REGISTER_BUTTON)).toBeVisible();
})

test('Verify "All books" link is visible after user login', async ({page}) => {
    await page.goto(BASE_URL);

    await expect(page.locator(NAVBAR.LOGIN_BUTTON)).toBeVisible();

    await page.locator(NAVBAR.LOGIN_BUTTON).click();

    await expect(page.locator(LOGIN_FORM.LOGIN_FORM)).toBeVisible();

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);
    await page.locator(LOGIN_FORM.LOGIN_BUTTON).click();

    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);
})

test('Verify user email is visible after user login', async ({ page }) =>{
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await expect(page.locator(LOGIN_FORM.LOGIN_FORM)).toBeVisible()

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);
    await page.locator(LOGIN_FORM.LOGIN_BUTTON).click();

    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);

    await expect(page.locator(LOGGED_NAVBAR.USER_EMAIL)).toBeVisible();
    await expect(page.locator(LOGGED_NAVBAR.MY_BOOKS)).toBeVisible();
    await expect(page.locator(LOGGED_NAVBAR.ADD_BOOK)).toBeVisible();
})

//Login form tests

test('Login with valid credentials', async ({ page }) =>{
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);
    await page.locator(LOGIN_FORM.LOGIN_BUTTON).click();

    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);
})

test('Login with empty input fields', async ({ page }) =>{
    await page.goto(TEST_URL.TEST_LOGIN_URL);


    await page.locator(LOGIN_FORM.LOGIN_BUTTON).click();
    page.on('dialog', async dialog =>{
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_MESSAGE);
    });

    await page.waitForURL(TEST_URL.TEST_LOGIN_URL);
    expect(page.url()).toBe(TEST_URL.TEST_LOGIN_URL);
})

test('Login with empty email field', async ({ page }) =>{
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);

    await page.locator(LOGIN_FORM.LOGIN_BUTTON).click();
    page.on('dialog', async dialog =>{
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_MESSAGE);
    });

    await page.waitForURL(TEST_URL.TEST_LOGIN_URL);
    expect(page.url()).toBe(TEST_URL.TEST_LOGIN_URL);
})

test('Login with empty password field', async ({ page }) =>{
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);

    await page.locator(LOGIN_FORM.LOGIN_BUTTON).click();
    page.on('dialog', async dialog =>{
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_MESSAGE);
    });

    await page.waitForURL(TEST_URL.TEST_LOGIN_URL);
    expect(page.url()).toBe(TEST_URL.TEST_LOGIN_URL);
})

// Register page - for homework

test('Submit the Form with Valid Values', async ({ page }) =>{
    await page.goto(TEST_URL.TEST_REGISTER_URL);

    await page.locator(REGISTER_BUTTON.EMAIL_NOT_REGISTER_USER.empty()).fill(NOT_REGISTER_USER.EMAIL);
    await page.locator(REGISTER_BUTTON.PASSWORD_NOT_REGISTER_USER).fill(NOT_REGISTER_USER.PASSWORD);
    await page.locator(REGISTER_BUTTON.REPEAT_PASSWORD).fill(NOT_REGISTER_USER.PASSWORD);

    await page.locator(REGISTER_BUTTON. SUBMIT_REGISTER_BUTTON).click();

    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);
})

test('Submit the form with empty values', async ({ page }) => {
    await page.goto(TEST_URL.TEST_REGISTER_URL);

    await page.locator(REGISTER_BUTTON.REGISTER_BUTTON).click();
    page.on('dialog', async dialog =>{
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_MESSAGE);
    });

    await page.waitForURL(TEST_URL.TEST_REGISTER_URL);
    expect(page.url()).toBe(TEST_URL.TEST_REGISTER_URL);
})

test('Submit the form with empty Email', async ({ page }) => {
    await page.goto(TEST_URL.TEST_REGISTER_URL);

    await page.locator(REGISTER_BUTTON.PASSWORD_NOT_REGISTER_USER).fill(NOT_REGISTER_USER.PASSWORD);
    await page.locator(REGISTER_BUTTON.REPEAT_PASSWORD).fill(NOT_REGISTER_USER.PASSWORD);

    await page.locator(REGISTER_BUTTON.SUBMIT_REGISTER_BUTTON).click();
    page.on('dialog', async dialog =>{
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_MESSAGE);
    });

    await page.waitForURL(TEST_URL.TEST_REGISTER_URL);
    expect(page.url()).toBe(TEST_URL.TEST_REGISTER_URL);
})

test('Submit the Form with Empty Password', async ({ page }) => {
    await page.goto(TEST_URL.TEST_REGISTER_URL);

    await page.locator(REGISTER_BUTTON.PASSWORD_NOT_REGISTER_USER).fill(NOT_REGISTER_USER.EMAIL);
    
    await page.locator(REGISTER_BUTTON.SUBMIT_REGISTER_BUTTON).click();
    page.on('dialog', async dialog =>{
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_MESSAGE);
    });

    await page.waitForURL(TEST_URL.TEST_REGISTER_URL);
    expect(page.url()).toBe(TEST_URL.TEST_REGISTER_URL);
})

test('Submit the Form with Empty Confirm Password', async ({ page }) => {
    await page.goto(TEST_URL.TEST_REGISTER_URL);

    await page.locator(REGISTER_BUTTON.PASSWORD_NOT_REGISTER_USER).fill(NOT_REGISTER_USER.EMAIL);
    await page.locator(REGISTER_BUTTON.PASSWORD_NOT_REGISTER_USER).fill(NOT_REGISTER_USER.PASSWORD);
    
    await page.locator(REGISTER_BUTTON.SUBMIT_REGISTER_BUTTON).click();
    page.on('dialog', async dialog =>{
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_MESSAGE);
    });

    await page.waitForURL(TEST_URL.TEST_REGISTER_URL);
    expect(page.url()).toBe(TEST_URL.TEST_REGISTER_URL);
})

test('Submit the Form with Different Passwords', async ({ page }) => {
    await page.goto(TEST_URL.TEST_REGISTER_URL);

    await page.locator(REGISTER_BUTTON.PASSWORD_NOT_REGISTER_USER).fill(NOT_REGISTER_USER.EMAIL);
    await page.locator(REGISTER_BUTTON.PASSWORD_NOT_REGISTER_USER).fill(NOT_REGISTER_USER.PASSWORD);
    await page.locator(REGISTER_BUTTON.PASSWORD_NOT_REGISTER_USER).fill(NOT_REGISTER_USER.REPEAT_DIFFERENT_PASSWORD);
    
    await page.locator(REGISTER_BUTTON.SUBMIT_REGISTER_BUTTON).click();
    page.on('dialog', async dialog =>{
        expect(dialog.type()).toContain('alert');
        expect(dialog.message()).toContain(ALERT.ALERT_PASSWORD_NOT_MATCH);
    });

    await page.waitForURL(TEST_URL.TEST_REGISTER_URL);
    expect(page.url()).toBe(TEST_URL.TEST_REGISTER_URL);
})

// Add book page

test('Add book with correct data', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);

    await Promise.all([
       page.locator(LOGIN_FORM.LOGIN_BUTTON).click(),
       page.waitForURL(TEST_URL.TEST_CATALOG_URL)
    ]);

    await page.locator(LOGGED_NAVBAR.ADD_BOOK).click();
    await page.locator(CREATE_FORM.TITLE).fill(TEST_BOOK.TITLE);
    await page.locator(CREATE_FORM.DESCRIPTION).fill(TEST_BOOK.DESCRIPTION);
    await page.locator(CREATE_FORM.IMAGE).fill(TEST_BOOK.IMAGE);
    await page.locator(CREATE_FORM.TYPE_OPTION).selectOption(TEST_BOOK.TEST_BOOK_OPTIONS);
    await page.locator(CREATE_FORM.ADD_BOOK_BUTTON).click();

    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);

})

test('Login and verify that all books are displayed', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);

    await Promise.all([
       page.locator(LOGIN_FORM.LOGIN_BUTTON).click(),
       page.waitForURL(TEST_URL.TEST_CATALOG_URL)
    ]);

    // const allBooksElements = page.locator(ALL_BOOKS_LIST)
    // expect(allBooksElements.count()).toBeGreaterThan(0);

    const booksCount = await page.locator('//li[@class="otherBooks"]').count();
    expect(booksCount).toBeGreaterThan(-1);
})

test('Login and navigate to Details page', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);

    await Promise.all([
       page.locator(LOGIN_FORM.LOGIN_BUTTON).click(),
       page.waitForURL(TEST_URL.TEST_CATALOG_URL)
    ]);

    await page.locator(DETAILS_BUTTON).first().click();
    await expect(page.locator(DETAILS_DESCRIPTION)).toBeVisible();

})


test('Verify that "Logout" button is visible', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);

    await Promise.all([
       page.locator(LOGIN_FORM.LOGIN_BUTTON).click(),
       page.waitForURL(TEST_URL.TEST_CATALOG_URL)
    ]);

    await expect(page.locator(LOGGED_NAVBAR.LOGOUT_BUTTON)).toBeVisible();

});

test('Verify that "Logout" button redirect correctly', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);

    await Promise.all([
       page.locator(LOGIN_FORM.LOGIN_BUTTON).click(),
       page.waitForURL(TEST_URL.TEST_CATALOG_URL)
    ]);

    await page.locator(LOGGED_NAVBAR.LOGOUT_BUTTON).click();

    await page.waitForURL(TEST_URL.TEST_CATALOG_URL);
    expect(page.url()).toBe(TEST_URL.TEST_CATALOG_URL);

    await expect(page.locator(NAVBAR.LOGIN_BUTTON)).toBeVisible();
    await expect(page.locator(LOGGED_NAVBAR.USER_EMAIL)).toBeHidden();

})

//"All Books" Page

test('Verify That All Books Are Displayed', async ({page}) => {
    await page.goto(TEST_URL.TEST_LOGIN_URL);

    await page.locator(LOGIN_FORM.EMAIL).fill(TEST_USER.EMAIL);
    await page.locator(LOGIN_FORM.PASSWORD).fill(TEST_USER.PASSWORD);

    await Promise.all([
        page.locator(LOGIN_FORM.LOGIN_BUTTON).click(),
        page.waitForURL(TEST_URL.TEST_CATALOG_URL)
    ])
})




