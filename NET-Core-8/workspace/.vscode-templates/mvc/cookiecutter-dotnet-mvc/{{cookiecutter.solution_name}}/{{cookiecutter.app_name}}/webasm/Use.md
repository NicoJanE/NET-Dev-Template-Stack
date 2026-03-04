**Source directory:** `./assembly`  
**Source file:** `*index.ts`  
**Recreate environment**
```bash
npm install
npm run asbuild
```
**Build:** ` npm run asbuild`   
> >**Result** in: ***build/release.wasm***  
>> **Copy files to:** `../../wwwroot/js/build/`
>>> - `release.wasm` (main compiled binary)
>>> - `release.js` (AssemblyScript loader - required for WASM to work with JS strings)  
>>> - `release.d.ts` (TypeScript types - optional, for IDE intellisense)

**Call in HTML** via JavaScript wrapper:
```html
<button onclick="checkTargetClass()">Check Target Class</button>
```

The wrapper function (in `wwwroot/js/site.js`) collects DOM data and passes it to WASM `searchForClass()` function.

<br><br>
Note I:   
**Initial install Assembly**: `npm install --save-dev assemblyscript`  

Note II:
For Wasm one should add these files to the `.gitignore` file:

``` bash 
node_modules/
package-lock.json
build/debug.*
 ```