# MatraIAST
_Fork of [PowerAccent](https://github.com/damienleroy/PowerAccent)_

_Previously known as **PowerIAST**_

## How it works
Press and hold letter then hit space bar or arrow key to select the accent. For example `e` & `⎵`. With spacebar, repeat stroke to change letter. When releasing the letter key, the accent is inserted.

The software is currently working with `a`, `d`, `e`, `h`, `i`, `l`, `m`, `n`, `o`, `r`, `s`, `t`, `u`.

## Requirements
This tool requires the following components to be installed to run:
- .NET 6.0 Desktop Runtime

## Download
### winget
You can install using winget: `winget install poweriast`.
The package name is: `Alphayama.PowerIAST`.

The setup file on winget is signed and won't get flagged as potential malware.

### Download from Releases section 
You may download the setup file from [Releases](https://github.com/alphayama/PowerIAST/releases) section!

Note: The setup will get flagged as potential malware as it is not signed. I am trying to find a way to fix this.

## Problems known
- The selector bar is not visible on the secondary display
- (Experimental feature) Inside browsers & some other softwares, the tool can't detect the caret position. So default is applied, meaning up center of the main window

## The future
- Trying to keep this project up to date with the original project
- Theme & design improvement

## Credits
I always wanted an easy to way to input Sanskrit transliteration diacritics on Windows and the [PowerAccent](https://github.com/damienleroy/PowerAccent) project by [damienleroy](https://github.com/damienleroy) helped me to achieve that.

If you like the project, donate to the developer of the original project:
https://www.buymeacoffee.com/dams 

The original project also received contributions from:
- [Ciantic](https://gist.github.com/Ciantic/471698) for the implementation of the Keyboard Listener.
- [Saurabh Singh](https://www.codeproject.com/Articles/34520/Getting-Caret-Position-Inside-Any-Application) to share how get the caret position.
- [PowerToys team](https://github.com/microsoft/PowerToys) to integrated PowerAccent in PowerToys (named as Quick Accent).
