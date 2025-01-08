const {fetchData} = require("./async_test_functions.js")

QUnit.test("with invalid postal code", async function(assert){
   const data = await fetchData("https://www.zippopotam.us/bg/8000");

   assert.notOk(data);
})

QUnit.test("with invalid url", async function(assert){
   const data = await fetchData("https://www.zippopotam.us/bg/8000");

   assert.notOk(data, "fetch failed");
})

