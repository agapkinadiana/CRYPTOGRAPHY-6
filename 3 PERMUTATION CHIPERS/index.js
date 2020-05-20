const Alphavit = [
    ['a', 'b', 'c', 'd'],
    ['e', 'f', 'g', 'h'],
    ['i', 'j', 'k', 'l'],
    ['m', 'n', 'o', 'p'],
    ['q', 'r', 's', 't'],
    ['u', 'v', 'w', 'x'], 
    ['y', 'z', ' ', ' ']
];
const Alphabet = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
                  'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
                  's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];

//[3]   1. Маршрутная перестановка (маршрут: зигзагом)  
//      2. Множественная перестановка, ключевые слова –  имя и фамилия 

console.log(`Aphabet: ${Alphavit}`);

function ZigWay(Alphavi){
    let Alpha = [];
    let n = 0;
    let arrCount = Alphavi.length;
    let count = Alphavi[0].length;
    for(let i = 0; i <= count; i++){
        if(i%2 == 1){
            for(let j = 0; j < arrCount; j++){
                Alpha[n++] = Alphavi[j][i];
                if(n == 27)
                break;
            }
        }
        else{
            for(let j = arrCount-1; j >= 0; j--){
                Alpha[n++] = Alphavi[j][i];
                if(n == 27)
                break;
            }
        }
        if(n == 27)
            break;
    }
    console.log(`Apha: ${Alpha}`);
    return Alpha;
}

function WaySH(key){
    console.log('WaySH');
    let timer = Date.now();
    let Alpha2 = ZigWay(Alphavit);
    key = key.toLowerCase();
    let keySH = [];
    let keySH2 = [];
    //
    // for(let i = 0; i < key.length ; i++){
    //     keySH[i] = Alpha[Alphabet.indexOf(key[i])];
    // }

    let Alpha = ZigWay(key);

    for(let i = 0; i < key.length ; i++){
         keySH[i] = Alpha[i];
    }

    for(let i = 0; i < key.length ; i++){
        keySH2[i] = Alpha2[i];
    }

    console.log('Key: ' + key);
    console.log('KeySH: ' + keySH.join(''));
    console.log('KeySH: ' + keySH2.join(''));
    console.log(`Time: ${Date.now() - timer}`)

    let ghystogram = {};

    for(let i = 0; i < keySH.length; i++){
        if(ghystogram[keySH[i]] == undefined){
            ghystogram[keySH[i]] = 1;
        }
        else{
            ghystogram[keySH[i]]++;
        }
    }

    let ghysto = document.getElementById('ghysto');
    ghysto.innerHTML = '';

    for(let letter in ghystogram){
        let percent = (ghystogram[letter]/keySH.length);
        DrowGhysto(ghysto, percent, letter)
        console.log(letter + ' = ' + ghystogram[letter]);
    }

    console.log('--------------------------------------------');
}

function ManySH(key1, key2){
    console.log('ManySH');
    let timer = Date.now();
    key1 = key1.toLowerCase();
    key2 = key2.toLowerCase();
    let keySH = [];

    let Alpha = new Array(key1.length);
    for(let i = 0; i < Alpha.length; i++){
        Alpha[i] = new Array(key2.length);
    }
    let AlphaSH = new Array(key1.length);
    for(let i = 0; i < AlphaSH.length; i++){
        AlphaSH[i] = new Array(key2.length);
    }
    let n = 0;
    for(let i = 0; i < key1.length; i++){
        for(let j = 0; j < key2.length; j++){
            if(n < Alphabet.length)
                Alpha[i][j] = Alphabet[n++];
            else
                Alpha[i][j] = ' ';
        }
    }

    let Akey1 = [];
    for(k in key1){
        Akey1[k] = key1[k];
    }
    Akey1.sort();
    for(k in key1){
        for(l in Akey1){
            if(Akey1[l] == key1[k])
                AlphaSH[k] = Alpha[l];
        }
    }

    let Akey2 = [];
    for(k in key2){
        Akey2[k] = key2[k];
    }

    Akey2.sort();

    for(k in key2){
        for(l in Akey2){
            if(Akey2[l] == key2[k])
                for(let i = 0; i < AlphaSH.length; i++){
                    AlphaSH[i][k] = Alpha[i][l];
                }
        }
    }
    for(l in AlphaSH){
        console.log('alphash '+l+': '+AlphaSH[l]);
    }

    keySH = ZigWay(AlphaSH);



    console.log('KeySH: ' + keySH.join(''));
    console.log(`Time: ${Date.now() - timer}`)

    let ghystogram = {};

    for(let i = 0; i < keySH.length; i++){
        if(ghystogram[keySH[i]] == undefined){
            ghystogram[keySH[i]] = 1;
        }
        else{
            ghystogram[keySH[i]]++;
        }
    }

    let ghysto = document.getElementById('ghysto');
    ghysto.innerHTML = '';

    for(let letter in ghystogram){
        let percent = (ghystogram[letter]/keySH.length);
        DrowGhysto(ghysto, percent, letter)
        console.log(letter + ' = ' + ghystogram[letter]);
    }

    console.log('--------------------------------------------');
}

function DrowGhysto(ghysto, percent, letter){
    let h = 200 * percent;
    let line = document.createElement('div');
    line.innerHTML = `
    <div style="width: 50px; height: ${h}px; background-color: #91c6e6;">
        ${Math.floor(percent*100)}%
    </div>
    <p>${letter}</p>`
    ghysto.append(line);
}
