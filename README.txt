ADown
=====================

A Facebook Downloader in C#. This uses the Facebook Graph API to get the albums of the user, match the specified album with the album URL supplied by the user, get its ID, and fetch the photo URLs using the API again.

Notes
---------------------
    * Downloading speed will depend on the connection speed
    * The download folder by default is in the Documents folder named 'ADown'
    * Each picture will be named based on the Photo ID in the Graph API

Features
---------------------
    * Threaded
    * Verbose
    * Uses Facebook Graph API
    * Able to change download folder
    * Creates a folder for every album
    * Accepts the Album URL (easy copy-paste)

TODO
---------------------
    * Still need to make the downloading faster by downloading in multiple threads
    * Make the user choose how many threads to use when downloading
    * More efficient photo naming

Compiler
---------------------
Microsoft Visual Studio 2010

Runtime Dependencies
---------------------
.NET Framework 4

External Classes
---------------------
BasicReq - https://gist.github.com/865237
JSON (parser) - http://techblog.procurios.nl/k/618/news/view/14605/14863/How-do-I-write-my-own-parser-for-JSON.html

License
---------------------
Copyright (c) 2011, Ruel Pagayon
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
	* Redistributions of source code must retain the above copyright
	  notice, this list of conditions and the following disclaimer.
	* Redistributions in binary form must reproduce the above copyright
	  notice, this list of conditions and the following disclaimer in the
	  documentation and/or other materials provided with the distribution.
	* Neither the name of the author nor the names of its contributors 
	  may be used to endorse or promote products derived from this software 
	  without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE AUTHOR AND CONTRIBUTORS "AS IS" AND ANY 
EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT, 
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT 
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF 
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE 
OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF 
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.