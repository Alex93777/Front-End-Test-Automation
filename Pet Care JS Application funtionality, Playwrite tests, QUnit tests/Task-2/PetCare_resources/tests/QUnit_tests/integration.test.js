const baseUrl = 'http://localhost:3030/';

let user = {
    email: '',
    password: '123456',
};

let token = '';
let userId = '';

let lastCreatedPostCardId = '';

let petPal = {
    age: '',
    breed: '',
    imageUrl: '/images/dog2.jpg',
    image: '/images/guinea-pig.jpg',
    name: '',
    weight: '',
}

QUnit.config.reorder = false;

QUnit.module("user functionalities", () => {
    QUnit.test("user registration", async (assert) => {
        //Arrange
        let path = 'users/register';
        let random = Math.floor(Math.random() * 10000);
        let email = `pet${random}@abv.bg`;

        user.email = email;

        //Act
        let response = await fetch(baseUrl + path, {
            method: "POST",
            headers: {
                'content-type': 'application/json'
            },
            body: JSON.stringify(user)
        });
        let json = await response.json();
        console.log(json);

        //Assert
        assert.ok(response.ok);

        assert.ok(json.hasOwnProperty('accessToken'), 'accessToken property exists');
        assert.strictEqual(typeof json.accessToken, 'string', "accessToken has correct type");

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

        token = json['accessToken'];
        userId = json['_id'];
        sessionStorage.setItem('pet-user', JSON.stringify(user));
    })

    QUnit.test("user login", async (assert) => {
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
        console.log(json);

        //assert
        assert.ok(response.ok);

        assert.ok(json.hasOwnProperty('accessToken'), 'accessToken property exists');
        assert.strictEqual(typeof json.accessToken, 'string', "accessToken has correct type");

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

        token = json['accessToken'];
        userId = json['_id'];
        sessionStorage.setItem('pet-user', JSON.stringify(user));
    })
})

QUnit.module("pet functionalities", () => {
    QUnit.test("get all Postcards", async (assert) => {
        //arrange
        let path = 'data/pets';
        let queryParams = '?sortBy=_createdOn%20desc&distinct=name';

        //act
        let response = await fetch(baseUrl + path + queryParams);
        let json = await response.json();

        //assert
        console.log(json);
        assert.ok(response.ok, "Response is successful");
        assert.ok(Array.isArray(json), "Response is array");

        json.forEach(jsonData => {
            assert.ok(jsonData.hasOwnProperty('age'), "age exists");
            assert.strictEqual(typeof jsonData.age, 'string', "age is from correct type");

            assert.ok(jsonData.hasOwnProperty('breed'), "breed exists");
            assert.strictEqual(typeof jsonData.breed, 'string', "breed is from correct type");

            assert.ok(jsonData.hasOwnProperty('image'), "image exists");
            assert.strictEqual(typeof jsonData.image, 'string', "image is from correct type");

            assert.ok(jsonData.hasOwnProperty('name'), "name exists");
            assert.strictEqual(typeof jsonData.name, 'string', "name is from correct type");

            assert.ok(jsonData.hasOwnProperty('weight'), "weight exists");
            assert.strictEqual(typeof jsonData.weight, 'string', "weight is from correct type");

            assert.ok(jsonData.hasOwnProperty('_createdOn'), "_createdOn exists");
            assert.strictEqual(typeof jsonData._createdOn, 'number', "_createdOn is from correct type");

            assert.ok(jsonData.hasOwnProperty('_id'), "_id exists");
            assert.strictEqual(typeof jsonData._id, 'string', "_id is from correct type");

            assert.ok(jsonData.hasOwnProperty('_ownerId'), "_ownerId exists");
            assert.strictEqual(typeof jsonData._ownerId, 'string', "_ownerId is from correct type");
        });
    })

    QUnit.test("Create Postcard", async (assert) => {
        //arrange
        let path = "data/pets";
        let random = Math.floor(Math.random() * 10000);
        petPal.age = '7';
        petPal.name = `Random pet name ${random}`;
        petPal.breed = 'Akita';
        petPal.weight = '25';

        //act
        let response = await fetch(baseUrl + path, {
            method: "POST",
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(petPal)
        });
        let json = await response.json();
        console.log(json);
        

         //assert
         assert.ok(response.ok, "Response is successfull");

         assert.ok(json.hasOwnProperty('age'), "age exists");
         assert.strictEqual(typeof json.age, 'string', "age is from correct type");

         assert.ok(json.hasOwnProperty('breed'), "breed exists");
         assert.strictEqual(typeof json.breed, 'string', "breed is from correct type");

         assert.ok(json.hasOwnProperty('imageUrl'), "imageUrl exists");
         assert.strictEqual(typeof json.imageUrl, 'string', "imageUrl is from correct type");

         assert.ok(json.hasOwnProperty('name'), "name exists");
         assert.strictEqual(typeof json.name, 'string', "name is from correct type");

         assert.ok(json.hasOwnProperty('weight'), "weight exists");
         assert.strictEqual(typeof json.weight, 'string', "weight is from correct type");

         assert.ok(json.hasOwnProperty('_createdOn'), "_createdOn exists");
         assert.strictEqual(typeof json._createdOn, 'number', "_createdOn is from correct type");

         assert.ok(json.hasOwnProperty('_id'), "_id exists");
         assert.strictEqual(typeof json._id, 'string', "_id is from correct type");

         assert.ok(json.hasOwnProperty('_ownerId'), "_ownerId exists");
         assert.strictEqual(typeof json._ownerId, 'string', "_ownerId is from correct type");

         lastCreatedPostCardId = json._id;
    })

    QUnit.test("Edit functionality", async (assert) => {
        //arrange
        let path = 'data/pets';
        let random = Math.floor(Math.random() * 10000);
        petPal.name = `Random (edited) name ${random}`;

        //act
        let response = await fetch(baseUrl + path + `/${lastCreatedPostCardId}`, {
            method: "PUT",
            headers: {
                'content-type': 'application/json',
                'X-Authorization': token
            },
            body: JSON.stringify(petPal)
        });
        let json = await response.json();
        console.log(json);
        

        //assert
        assert.ok(response.ok, "Response is successfull");

         assert.ok(json.hasOwnProperty('age'), "age exists");
         assert.strictEqual(typeof json.age, 'string', "age is from correct type");

         assert.ok(json.hasOwnProperty('breed'), "breed exists");
         assert.strictEqual(typeof json.breed, 'string', "breed is from correct type");

         assert.ok(json.hasOwnProperty('imageUrl'), "imageUrl exists");
         assert.strictEqual(typeof json.imageUrl, 'string', "imageUrl is from correct type");

         assert.ok(json.hasOwnProperty('name'), "name exists");
         assert.strictEqual(typeof json.name, 'string', "name is from correct type");

         assert.ok(json.hasOwnProperty('weight'), "weight exists");
         assert.strictEqual(typeof json.weight, 'string', "weight is from correct type");

         assert.ok(json.hasOwnProperty('_createdOn'), "_createdOn exists");
         assert.strictEqual(typeof json._createdOn, 'number', "_createdOn is from correct type");

         assert.ok(json.hasOwnProperty('_id'), "_id exists");
         assert.strictEqual(typeof json._id, 'string', "_id is from correct type");

         assert.ok(json.hasOwnProperty('_ownerId'), "_ownerId exists");
         assert.strictEqual(typeof json._ownerId, 'string', "_ownerId is from correct type");
    })

    QUnit.test("Delete functionality", async (assert) => {
        //arrange
        let path = "data/pets";

        //act
        let response = await fetch(baseUrl + path + `/${lastCreatedPostCardId}`, {
            method: "DELETE",
            headers: {
                'X-Authorization': token
            },
        });

        //assert
        assert.ok(response.ok, "Response is successfull");
    })
})