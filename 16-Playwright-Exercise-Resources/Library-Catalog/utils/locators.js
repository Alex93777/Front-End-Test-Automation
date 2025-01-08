const NAVBAR = {
    NAV_NAVBAR: 'nav.navbar',
    ALL_BOOKS_LINK: 'a[href = "/catalog"]',
    LOGIN_BUTTON: 'a[href = "/login"]',
    REGISTER_BUTTON: 'a[href = "/register"]'
}

const LOGIN_FORM = {
    LOGIN_FORM: '#login-form',
    EMAIL: 'input[id="email"]',
    PASSWORD: 'input[id="password"]',
    LOGIN_BUTTON: '#login-form input[type="submit"]'
}

const LOGGED_NAVBAR = {
    USER_EMAIL: '//span[text()="Welcome, peter@abv.bg"]',
    MY_BOOKS: 'a[href="/profile"]',
    ADD_BOOK: 'a[href="/create"]',
    LOGOUT_BUTTON: 'a[id="logoutBtn"]'
}

const CREATE_FORM = {
    TITLE:  "input[id='title']",
    DESCRIPTION: "textarea[id='description']",
    IMAGE: "input[id='image']",
    TYPE_OPTION: "#type",
    ADD_BOOK_BUTTON: "#create-form input[type='submit']"
}

const REGISTER_BUTTON = {
    REGISTER_BUTTON: '//*[@id="guest"]/a[2]',
    EMAIL_NOT_REGISTER_USER: '//*[@id="email"]',
    PASSWORD_NOT_REGISTER_USER: '//*[@id="password"]',
    REPEAT_PASSWORD: '//*[@id="repeat-pass"]',
    SUBMIT_REGISTER_BUTTON: '//*[@id="register-form"]/fieldset/input'
};

const MY_BOOKS_BUTTON = {
    MY_BOOKS_BUTTON: '//*[@id="user"]/a[1]'
}

const ALL_BOOKS_LIST = '//li@[class="otherBooks"]';

const DETAILS_BUTTON = '//a[text()="Details"]';

const DETAILS_DESCRIPTION = '//h3[text()="Description:"]';

export {
    NAVBAR,
    LOGIN_FORM,
    LOGGED_NAVBAR,
    CREATE_FORM,
    ALL_BOOKS_LIST,
    DETAILS_BUTTON,
    DETAILS_DESCRIPTION,
    REGISTER_BUTTON,
    MY_BOOKS_BUTTON
}