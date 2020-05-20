const rotor = [
    ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
    'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
    's', 't', 'u', 'v', 'w', 'x', 'y', 'z'],
    ['l', 'e', 'y', 'j', 'v', 'c', 'n', 'i', 'x',
    'w', 'p', 'b', 'q', 'm', 'd', 'r', 't', 'a',
    'k', 'z', 'g', 'f', 'u', 'h', 'o', 's'],
    ['f', 's', 'o', 'k', 'a', 'n', 'u', 'e', 'r',
    'h', 'm', 'b', 't', 'i', 'y', 'c', 'w', 'l',
    'q', 'p', 'z', 'x', 'v', 'g', 'j', 'd'],
    ['v', 'z', 'b', 'r', 'g', 'i', 't', 'y', 'u',
    'p', 's', 'd', 'n', 'h', 'l', 'z', 'a', 'w',
    'm', 'j', 'q', 'o', 'f', 'e', 'c', 'k']
];
const reflector = {
    'a': 'e', 'b': 'n', 'c': 'k', 'd': 'q',
    'f': 'u', 'g': 'y', 'h': 'w', 'i': 'j',
    'l': 'o', 'm': 'p', 'r': 'x', 's': 'z', 't': 'v'
};

const rotorShifts = [0, 2, 2];
//[7] Beta Gamma V B Dunn 0-2-2

function shiftRotor(n){
    temp = [];
    shCount = rotorShifts[n-1];
    for(let i = 0; i < rotor[n].length; i++){
        temp[i] = (i-shCount<0)?
            rotor[n][rotor[n].length + (i-shCount)] : rotor[n][i-shCount];
    }
    for(let i = 0; i < rotor[n].length; i++){
        rotor[n][i] = temp[i];
    }
}

function Enigma(key){
    console.log('Enigma');
    key = key.toLowerCase();
    let keySH = [];
    for(e in key){
        keySH[e] = key[e];
    }

    for(let k = 0; k < key.length; k++){
        for(let n = rotor.length-1; n > 0; n--){
            keySH[k] = rotor[n][rotor[0].indexOf(keySH[k])];
        }

        for(e in reflector){
            if(keySH[k] == reflector[e]){
                keySH[k] = e;
            }
            else if(keySH[k] == e){
                keySH[k] = reflector[e];
            }   
        }

        for(let n = 1; n < rotor.length; n++){
            keySH[k] = rotor[n][rotor[0].indexOf(keySH[k])];
        }

        for(let i = 1; i < rotor.length; i++){
            shiftRotor(i);
        }
    }

    console.log('keySH: ' + keySH.join(''));
    console.log('key: ' + key);

    console.log('--------------------------------------------');
}
