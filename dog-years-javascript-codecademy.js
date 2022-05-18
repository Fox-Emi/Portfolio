let humanAge = 19;
let dogAge = 0;
let myName = "Emilia";
for(let i = 0; i<2; i++){
  humanAge--;
  dogAge += 10.5;
}
dogAge += 4 * humanAge;
console.log(`My name is ${myName}. I am ${humanAge+2} years old. That's ${dogAge} years in dog years!`);