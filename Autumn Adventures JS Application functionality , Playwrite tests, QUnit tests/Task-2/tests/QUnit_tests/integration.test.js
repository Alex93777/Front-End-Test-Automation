const baseUrl = 'http://localhost:3030/';

let user = {
    email: '',
    password: '123456',
};

let token = '';
let userId = '';

let lastCreatedBookId = '';
let book = {
    title: '',
    description: '',
    imageUrl: '/images/book.png',
    type: "Other"
}

QUnit.config.reorder = false;

QUnit.module("user functionalities", () => {
    QUnit.test("user registration", async (assert) => {
        //arrange
        let path = 'users/register';
        let random = Math.floor(Math.random() * 10000);
        let email = `abv${random}@abv.bg`;

        user.email = email;

        //act
        let response = await fetch(baseUrl + path, {
            method: "POST",
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        let json = await response.json();

        //assert
        console.log(json);
        assert.ok(response.ok);

        assert.ok(json.hasOwnProperty('email'), 'Email property exists');
        assert.equal(json['email'], user.email, 'email has correct value');
        assert.strictEqual(typeof json.email, 'string', "email has correct type");

        assert.ok(json.hasOwnProperty('password'), 'Password property exists');
        assert.equal(json['password'], user.password, 'password has correct value');
        assert.strictEqual(typeof json.password, 'string', "password has correct type");

        assert.ok(json.hasOwnProperty('_createdOn'), '_createdOn property exists');
        assert.strictEqual(typeof json._createdOn, 'number', "_createdOn has correct type");

        assert.ok(json.hasOwnProperty('_id'), '_id property exists');
        assert.strictEqual(typeof json._id, 'string', "_id has correct type");

        assert.ok(json.hasOwnProperty('accessToken'), 'accessToken property exists');
        assert.strictEqual(typeof json.accessToken, 'string', "accessToken has correct type");

        token = json['accessToken'];
        userId = json['_id'];
        sessionStorage.setItem('book-user', JSON.stringify(user));
    })

    QUnit.test('user login', async (assert) => {
        //arrange
        let path = 'users/login';

        //act
        let response = await fetch(baseUrl + path, {
            method: "POST",
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        let json = await response.json();

        console.log('login response');
        console.log(json);

        //assert
        assert.ok(json.hasOwnProperty('email'), 'Email property exists');
        assert.equal(json['email'], user.email, 'email has correct value');
        assert.strictEqual(typeof json.email, 'string', "email has correct type");

        assert.ok(json.hasOwnProperty('password'), 'Password property exists');
        assert.equal(json['password'], user.password, 'password has correct value');
        assert.strictEqual(typeof json.password, 'string', "password has correct type");

        assert.ok(json.hasOwnProperty('_createdOn'), '_createdOn property exists');
        assert.strictEqual(typeof json._createdOn, 'number', "_createdOn has correct type");

        assert.ok(json.hasOwnProperty('_id'), '_id property exists');
        assert.strictEqual(typeof json._id, 'string', "_id has correct type");

        assert.ok(json.hasOwnProperty('accessToken'), 'accessToken property exists');
        assert.strictEqual(typeof json.accessToken, 'string', "accessToken has correct type");

        token = json['accessToken'];
        userId = json['_id'];
        sessionStorage.setItem('book-user', JSON.stringify(user));
    })
})

QUnit.module("book functionalities", () => {
    QUnit.test("get all books", async (assert) => {
        //arrange
        let path = 'data/books';
        let queryParams = '?sortBy=_createdOn%20desc';

        //act
        let response = await fetch(baseUrl + path + queryParams);
        let json = await response.json();

        //assert
        console.log(json);
        assert.ok(response.ok, "Response is successful");
        assert.ok(Array.isArray(json), "Response is array");

        json.forEach(jsonData => {
            assert.ok(jsonData.hasOwnProperty('description'), "Description exists");
            assert.strictEqual(typeof jsonData.description, 'string', "description is from correct type");

            assert.ok(jsonData.hasOwnProperty('imageUrl'), "imageUrl exists");
            assert.strictEqual(typeof jsonData.imageUrl, 'string', "imageUrl is from correct type");

            assert.ok(jsonData.hasOwnProperty('title'), "title exists");
            assert.strictEqual(typeof jsonData.title, 'string', "title is from correct type");

            assert.ok(jsonData.hasOwnProperty('type'), "type exists");
            assert.strictEqual(typeof jsonData.type, 'string', "type is from correct type");

            assert.ok(jsonData.hasOwnProperty('_createdOn'), "_createdOn exists");
            assert.strictEqual(typeof jsonData._createdOn, 'number', "_createdOn is from correct type");

            assert.ok(jsonData.hasOwnProperty('_id'), "_id exists");
            assert.strictEqual(typeof jsonData._id, 'string', "_id is from correct type");

            assert.ok(jsonData.hasOwnProperty('_ownerId'), "_ownerId exists");
            assert.strictEqual(typeof jsonData._ownerId, 'string', "_ownerId is from correct type");
        });
    })

    QUnit.test("create book", async (assert) => {
        //arrange
        let path = "data/books";
        let random = Math.floor(Math.random() * 10000);
        book.title = `Random book title ${random}`;
        book.description = `Random book description ${random}`;

        //act
        let response = await fetch(baseUrl + path, {
            method: "POST",
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(book)
        });
        let json = await response.json();

        //assert
        assert.ok(response.ok, "Response is successfull");

        assert.ok(json.hasOwnProperty('description'), "Description exists");
        assert.strictEqual(json.description, book.description, "Description has correct value");
        assert.strictEqual(typeof json.description, 'string', "description is from correct type");

        assert.ok(json.hasOwnProperty('imageUrl'), "imageUrl exists");
        assert.strictEqual(json.imageUrl, book.imageUrl, "imageUrl has correct value");
        assert.strictEqual(typeof json.imageUrl, 'string', "imageUrl is from correct type");

        assert.ok(json.hasOwnProperty('title'), "title exists");
        assert.strictEqual(json.title, book.title, "title has correct value");
        assert.strictEqual(typeof json.title, 'string', "title is from correct type");

        assert.ok(json.hasOwnProperty('type'), "type exists");
        assert.strictEqual(json.type, book.type, "type has correct value");
        assert.strictEqual(typeof json.type, 'string', "type is from correct type");

        assert.ok(json.hasOwnProperty('_createdOn'), "_createdOn exists");
        assert.strictEqual(typeof json._createdOn, 'number', "_createdOn is from correct type");

        assert.ok(json.hasOwnProperty('_id'), "_id exists");
        assert.strictEqual(typeof json._id, 'string', "_id is from correct type");

        assert.ok(json.hasOwnProperty('_ownerId'), "_ownerId exists");
        assert.strictEqual(json._ownerId, userId, "_ownerId has correct value");
        assert.strictEqual(typeof json._ownerId, 'string', "_ownerId is from correct type");

        lastCreatedBookId = json._id;
    })

    QUnit.test("Edit functionality", async (assert) => {
        //arrange
        let path = 'data/books';
        let random = Math.floor(Math.random() * 10000);
        book.title = `Random book edited title ${random}`;

        //act
        let response = await fetch(baseUrl + path + `/${lastCreatedBookId}`, {
            method: "PUT",
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(book)
        });
        let json = await response.json();

        //assert
        assert.ok(response.ok, "Response is successfull");

        assert.ok(json.hasOwnProperty('description'), "Description exists");
        assert.strictEqual(json.description, book.description, "Description has correct value");
        assert.strictEqual(typeof json.description, 'string', "description is from correct type");

        assert.ok(json.hasOwnProperty('imageUrl'), "imageUrl exists");
        assert.strictEqual(json.imageUrl, book.imageUrl, "imageUrl has correct value");
        assert.strictEqual(typeof json.imageUrl, 'string', "imageUrl is from correct type");

        assert.ok(json.hasOwnProperty('title'), "title exists");
        assert.strictEqual(json.title, book.title, "title has correct value");
        assert.strictEqual(typeof json.title, 'string', "title is from correct type");

        assert.ok(json.hasOwnProperty('type'), "type exists");
        assert.strictEqual(json.type, book.type, "type has correct value");
        assert.strictEqual(typeof json.type, 'string', "type is from correct type");

        assert.ok(json.hasOwnProperty('_createdOn'), "_createdOn exists");
        assert.strictEqual(typeof json._createdOn, 'number', "_createdOn is from correct type");

        assert.ok(json.hasOwnProperty('_id'), "_id exists");
        assert.strictEqual(typeof json._id, 'string', "_id is from correct type");

        assert.ok(json.hasOwnProperty('_updatedOn'), "_updatedOn exists");
        assert.strictEqual(typeof json._updatedOn, 'number', "_updatedOn is from correct type");

        assert.ok(json.hasOwnProperty('_ownerId'), "_ownerId exists");
        assert.strictEqual(json._ownerId, userId, "_ownerId has correct value");
        assert.strictEqual(typeof json._ownerId, 'string', "_ownerId is from correct type");
    })

    QUnit.test("Delete functionality", async (assert) => {
        //arrange
        let path = "data/books";

        //act
        let response = await fetch(baseUrl + path + `/${lastCreatedBookId}`, {
            method: "DELETE",
            headers: {
                'X-Authorization': token
            },
        });

        //assert
        assert.ok(response.ok, "Response is successfull");
    })
})