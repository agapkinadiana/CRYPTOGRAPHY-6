//
//  main.swift
//  SHA1
//
//  Created by Diana Agapkina on 5/24/20.
//  Copyright © 2020 Diana Agapkina. All rights reserved.
//

import Foundation

var str = "Agapkina"
print("String =  \(str)");
var hash = SHA1.hexString(from: str);
print("Hash =  \(hash!)");
