const fs = require('fs');

let firstTask = fs.readFileSync('./text1.txt');
let firstTaskEnglish = fs.readFileSync('./text2.txt');
const secondTask = fs.readFileSync('./text3.txt');
const thirdTask = fs.readFileSync('./text5.txt');
const regExpSimbols = /[ |0-9|,|;|:|\/|'|.|~|"|(|)|=|-|—|^|?|*|&|%|$|#|!|@|+|\||<|>|\\|\r|\n|\t]/g;

const alfabhetRussian = 'абвгдеёжзийклмнопрстуфхцчшщъыьэюя';
const alfabhetName = 'агпкиндсерв';
const alfabhetEnglish = 'abcdefghijklmnopqrstuvwxyz';
const alfabhetBinary = '01';

firstTask = firstTask.toString().toLowerCase().replace(regExpSimbols, '');
firstTaskEnglish = firstTaskEnglish.toString().toLowerCase().replace(regExpSimbols, '');

let hartley = n => Math.log2(n);

let shanon = (str, alfabhet) => {
  let H = 0;
  
  for(let i = 0; i < alfabhet.length; i++) {    
    let symbol = alfabhet.charAt(i), 
        s = new RegExp(symbol, 'g'),
        p = (str.match(s) === null) ? 0 : str.match(s).length / str.length;
    
    console.log(`символ: '${symbol}', p(${symbol}) = ${p}`);
    if(p !== 0) {
      H += p * Math.log2(p);
    } 
  }

  return -H;
};

let shanonByName = (name, alfabhet) => name.length * shanon(name, alfabhet);
let hartleyByName = (name, alfabhet) => name.length * hartley(alfabhet.length);

let lastTask = someNumber => {
  const p = someNumber;
  const q = 1 - p;

  const h = (- p * Math.log2(p) - q * Math.log2(q)) || 0;

  return (1 - h);
};

const str = 'Агапкина Диана Сергеевна';
const str1 = str.toLowerCase().replace(regExpSimbols, '');

console.log(`-----------------Задание 1---------------`);
console.log(`Длина текста = ${firstTask.length}`);
console.log(`Энтропия по Шеннону -russian-: ${shanon(firstTask, alfabhetRussian)}`);
console.log(`Энтропия по Хартли -russian-: ${hartley(alfabhetRussian.length)}`);
console.log(`Энтропия по Шеннону -english-: ${shanon(firstTaskEnglish, alfabhetEnglish)}`);
console.log(`Энтропия по Хартли -english-: ${hartley(alfabhetEnglish.length)}`);

console.log(`-----------------Задание 2---------------`);
console.log('Энтропия бинарного алфавита:', shanon(secondTask.toString(), alfabhetBinary));

console.log(`-----------------Задание 3---------------`);
console.log(`Количество информации(по Шеннону): ${shanonByName(str1, alfabhetName)}`);
console.log(`Количество информации(по Хартли): ${hartleyByName(str1, alfabhetName)}`);
console.log(`Количество информации(в бинарном виде): ${shanonByName(thirdTask.toString(), alfabhetBinary)}`);

console.log(`-----------------Задание 4---------------`);
console.log("ФИО при 0,1", lastTask(0.1) * str1.length);
console.log("ФИО при 0,5", lastTask(0.5) * str1.length);
console.log("ФИО при 1",   lastTask(1.0) * str1.length);
