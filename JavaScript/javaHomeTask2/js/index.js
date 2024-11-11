// function hello() {
//   console.log("Hello");
// }

// hello();
///////////////////TASK 1
// const compare = (number1, number2) => {
//   number1 = 7;
//   number2 = 7;

//   if (number1 < number2) {
//     return -1;
//   }
//   if (number1 > number2) {
//     return 1;
//   }
//   if (number1 === number2) {
//     return 0;
//   }
// };
// console.log(compare(2, 5));
//////////////////////TASK 2

// const factorial = (number) => {
//   if (number === 0 || number === 1) return 1;
//   return number * factorial(number - 1);
// };
// console.log(factorial(3));

/////////////////////TASK 3
// const single_to_three_digit = (number1, number2, number3) => {
//   number1 = 1;
//   number2 = 4;
//   number3 = 9;

//   three_digit = number1 * 100 + number2 * 10 + number3;
//   return three_digit;
// };
// console.log(single_to_three_digit());

///////////////////////TASK 4

// const rectangle_area = (length, width) => {
//   length = 5;
//   width = 5;
//   if (length === width) {
//     return length ** 2 && width ** 2;
//   }
//   if (length != width) {
//     return length * width;
//   }
// };
// console.log(rectangle_area());

///////////////////////TASK 5

// const isPerfectNumber = (number) => {
//   let sum = 0;

//   for (let i = 1; i <= number / 2; i++) {
//     if (number % 1 === 0) {
//       sum += i;
//     }
//   }

//   return sum === number;
// };
// console.log(isPerfectNumber(6));
// console.log(isPerfectNumber(12));

/////////////////////////TASK 6

// const isPerfectNumber = (number) => {
//   let sum = 0;

//   for (let i = 1; i <= number / 2; i++) {
//     if (number % i === 0) {
//       sum += i;
//     }
//   }

//   return sum === number;
// };

// const PerfectNumbersInRange = (min, max) => {
//   let result = "";

//   for (let i = min; i <= max; i++) {
//     if (isPerfectNumber(i)) {
//       result += i + " ";
//     }
//   }

//   return result.trim();
// };

// console.log(PerfectNumbersInRange(1, 100));

//////////////////////////////////TASK 7
// const formatTime = (hours, minutes = 0, seconds = 0) => {
//   const formatNumber = (num) => {
//     if (num < 10) {
//       return "0" + num;
//     } else {
//       return String(num);
//     }
//   };

//   return (
//     formatNumber(hours) +
//     ":" +
//     formatNumber(minutes) +
//     ":" +
//     formatNumber(seconds)
//   );
// };

// console.log(formatTime(9, 5, 3));
// console.log(formatTime(12));
// console.log(formatTime(6, 15));

///////////////////////////////////TASK 8
// const convertToSeconds = (hours = 0, minutes = 0, seconds = 0) => {
//   return hours * 3600 + minutes * 60 + seconds;
// };
// console.log(convertToSeconds(2));

///////////////////////////////////TASK 9

// const timeFormat = (seconds) => {
//   const hours = Math.floor(seconds / 3600);
//   const minutes = Math.floor((seconds % 3600) / 60);
//   const secs = seconds % 60;

//   let formattedHours;
//   if (hours < 10) {
//     formattedHours = "0" + hours;
//   } else {
//     formattedHours = hours;
//   }

//   let formattedMinutes;
//   if (minutes < 10) {
//     formattedMinutes = "0" + minutes;
//   } else {
//     formattedMinutes = minutes;
//   }

//   let formattedSeconds;
//   if (secs < 10) {
//     formattedSeconds = "0" + secs;
//   } else {
//     formattedSeconds = secs;
//   }

//   return formattedHours + ":" + formattedMinutes + ":" + formattedSeconds;
// };
// console.log(timeFormat(2000));

///////////////////////////////////TASK 10

// function dateToSeconds(hours, minutes, seconds) {
//   return hours * 3600 + minutes * 60 + seconds;
// }

// const timeFormat = (seconds) => {
//   const hours = Math.floor(seconds / 3600);
//   const minutes = Math.floor((seconds % 3600) / 60);
//   const secs = seconds % 60;

//   let formattedHours;
//   if (hours < 10) {
//     formattedHours = "0" + hours;
//   } else {
//     formattedHours = hours;
//   }

//   let formattedMinutes;
//   if (minutes < 10) {
//     formattedMinutes = "0" + minutes;
//   } else {
//     formattedMinutes = minutes;
//   }

//   let formattedSeconds;
//   if (secs < 10) {
//     formattedSeconds = "0" + secs;
//   } else {
//     formattedSeconds = secs;
//   }

//   return formattedHours + ":" + formattedMinutes + ":" + formattedSeconds;
// };

// function timeDifference(h1, m1, s1, h2, m2, s2) {
//   const seconds1 = dateToSeconds(h1, m1, s1);
//   const seconds2 = dateToSeconds(h2, m2, s2);

//   const differenceInSeconds = Math.abs(seconds1 - seconds2);

//   return timeFormat(differenceInSeconds);
// }
// console.log(timeDifference(10, 30, 15, 14, 25, 30));
//////////////////////////////////////////////////////////////////|
/////////////////////////****PART II****//////////////////////////|
//////////////////////////////////////////////////////////////////|

//////////////TASK 1
// const power_number = (number, power) => {
//   let result = 1;

//   if (power > 0) {
//     for (let i = 0; i < power; i++) {
//       result *= number;
//     }
//   }
//   return result;
// };
// console.log(power_number(3, 3));
/////////////TASK 2

// const divider = (number1, number2) => {
//   while (number2 !== 0) {
//     const temp = number2;
//     number2 = number1 % number2;
//     number1 = temp;
//   }

//   return number1;
// };
// console.log(divider(8, 4));

/////////////////TASK 3
// function findMaxDigit(num) {
//   return Math.max(...String(num).split("").map(Number));
// }

// console.log(findMaxDigit(123456789));

////////////////////TASK 4

// const primeNumber = (number) => {
//   if (number <= 1) {
//     return false;
//   }
//   if (number === 2) {
//     return true;
//   }

//   for (let i = 2; i <= Math.sqrt(number); i++) {
//     if (number % i === 0) {
//       return false;
//     }
//   }
//   return true;
// };
// console.log(primeNumber(11));
// console.log(primeNumber(15));

///////////////////////////TASK 5

// const getFactors = (number) => {
//   let array = [];
//   let divisor = 2;
//   let index = 0;

//   while (number > 1) {
//     while (number % divisor === 0) {
//       array[index] = divisor;
//       index++;
//       number /= divisor;
//     }
//     divisor++;
//   }
//   return array;
// };
// console.log(getFactors(36));

////////////////////////TASK 6
// const fibonacci = (number) => {
//   if (number <= 0) {
//     return 0;
//   }
//   if (number === 1 || number === 2) {
//     return 1;
//   }

//   return fibonacci(number - 1) + fibonacci(number - 2);
// };
// console.log(fibonacci(3));
