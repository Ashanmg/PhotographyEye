// Generated by typings
// Source: https://raw.githubusercontent.com/DefinitelyTyped/DefinitelyTyped/e05e1f6f83c6ac84e9d7ff8284e8089d53a788a6/compression/compression.d.ts
declare module "compression" {
    import express = require('express');

    namespace e {
        interface CompressionOptions  {
            /**
             * See https://github.com/expressjs/compression#chunksize regarding the usage.
             */
            chunkSize?: number;
    
            /**
             * See https://github.com/expressjs/compression#level regarding the usage.
             */
            level?: number;
    
            /**
             * See https://github.com/expressjs/compression#memlevel regarding the usage.
             */
            memLevel?: number;
    
            /**
             * See https://github.com/expressjs/compression#strategy regarding the usage.
             */
            strategy?: number;
    
            /**
             * See https://github.com/expressjs/compression#threshold regarding the usage.
             */
            threshold?: number|string;
    
            /**
             * See https://github.com/expressjs/compression#windowbits regarding the usage.
             */
            windowBits?: number;
    
            /**
             * See https://github.com/expressjs/compression#filter regarding the usage.
             */
            filter?: Function;
            
            /**
             * See https://nodejs.org/api/zlib.html#zlib_class_options regarding the usage.
             */
            flush?: number;
    
            /**
             * See https://nodejs.org/api/zlib.html#zlib_class_options regarding the usage.
             */
            finishFlush?: number;
        }
    }

    function e(options?: e.CompressionOptions): express.RequestHandler;
    export = e;
}
