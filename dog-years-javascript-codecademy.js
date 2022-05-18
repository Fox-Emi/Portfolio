let humanAge = 19; //my age
let dogAge = 0; //variable for my dog age, set to 0 to make it an integer
let myName = "Emilia"; //my name
for(let i = 0; i<2; i++){ //loops twice as per the 'first two years are equal to 10.5 dog years'
  humanAge--; //decrements my age
  dogAge += 10.5; //adds 10.5 to my dogAge
}
dogAge += 4 * humanAge; //adds 4 for the remaining years.
console.log(`My name is ${myName}. I am ${humanAge+2} years old. That's ${dogAge} years in dog years!`); //logs my name, human age and dog age.