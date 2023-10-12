using System;
using System.IO;
using Wasmtime;

using var engine = new Engine();

// Load the WebAssembly module from a file
using var module = Module.FromFile(engine, "/home/renato/wasm-interop-poc/rust_sum/pkg/rust_sum_bg.wasm");

using var linker = new Linker(engine);
using var store = new Store(engine);

var instance = linker.Instantiate(store, module);
var fn = instance.GetFunction<int, int, int>("add");

// Call a function exported by the module
if (fn == null) {
    Console.WriteLine("Function could not be loaded from wasm");
    return;
}


var result = fn(2, 3);

// Print the result
Console.WriteLine(result);
