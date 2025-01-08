import { expect } from 'chai';
import { isOddOrEven } from '../evenOrOdd.js';

describe('Even or Odd', () => {
    it('Should return odd when given an odd lenght string', () => {
        //Arrange
        const input = 'aaa';
        const expectedResult = 'odd';

        //Act
        const actualResult = isOddOrEven(input);

        //Assert
        expect(actualResult).to.equal(expectedResult);

    });

    it('Should return even when given an even length string', () => {
        
        const result = isOddOrEven('bbbb')

        expect(result).to.equal('even');
    })

    it('Should return undefined when non string input is given', () => {

        expect(isOddOrEven(7)).to.be.undefined;
        expect(isOddOrEven(true)).to.be.undefined;
        expect(isOddOrEven([1])).to.be.undefined;
        expect(isOddOrEven({})).to.be.undefined;
        expect(isOddOrEven(null)).to.be.undefined;
        expect(isOddOrEven()).to.be.undefined;
        expect(isOddOrEven(NaN)).to.be.undefined;

    })

    it('Should return even when empty string is given', () => {

        expect(isOddOrEven('')).to.equal('even');

    });

    it('Should return different result when invoked with different ouputs', () => {
        expect(isOddOrEven('foo')).to.equal('odd');
        expect(isOddOrEven('foobar')).to.equal('even');
    })
});