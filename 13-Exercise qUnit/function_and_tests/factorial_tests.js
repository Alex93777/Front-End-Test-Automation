const { factorial } = require("./test_functions.js")

QUnit.module("factorial function tests", () =>{
    QUnit.test("Positive number", function(assert){
        assert.equal(factorial(5), 120, "Positive number")
    })

    QUnit.test("Zero number", function(assert){
        assert.equal(factorial(0), 1, "Zero number")
    })

    QUnit.test("negative number", function(assert){
        assert.equal(factorial(-1), 1, "negative number")
    })

    QUnit.test("bigger number", function(assert){
        assert.equal(factorial(8), 40320, "bigger number")
    })
})