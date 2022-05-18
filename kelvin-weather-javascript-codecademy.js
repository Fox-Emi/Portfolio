const kelvin = 0;
/*
Used to convert from Kelvin to Celsius or Fahrenheit.
 */
var celsius = kelvin - 273;
//celsius conversion
var fahrenheit = celsius * (9 / 5) + 32;
//fahrenheit conversion;
fahrenheit = Math.floor(fahrenheit);
//rounds down fahrenheit to the closest whole number.
console.log(`The temperature is ${fahrenheit} degrees Fahrenheit.`);
var Newton = Math.floor(celsius * (33 / 100));
console.log(`The temperature is ${Newton} degrees Newton.`);