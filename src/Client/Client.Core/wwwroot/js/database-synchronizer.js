(async function () {
    const db = {
        init: false,
        cache: await caches.open('EatCalculatorCache'),
        getFSRef: () => window.Module.FS
    };

    window.db = db;

    db.synchronizeDbWithCache = async function (filename) {
        try {

            const fs = db.getFSRef();
            const backupPath = `${filename}`;
            const cachePath = `/data/cache/${backupPath.split('.')[0]}.db`;
            console.log(`Processing ${backupPath}...`);

            if (!db.init) {

                db.init = true;

                console.log("Checking cache...");

                const resp = await db.cache.match(cachePath);

                if (resp && resp.ok) {

                    const res = await resp.arrayBuffer();
                    //const res = await resp.blob();

                    if (res) {
                        fs.writeFile(backupPath, new Uint8Array(res));
                        const size = fs.stat(backupPath).size;
                        console.log(`Restored ${size} bytes from cache.`);
                        return 0;
                    }
                }

                console.log("No cache available.");
                return -1;
            }

            if (fs.analyzePath(backupPath).exists) {

                // give files time to flush
                const waitFlush = new Promise((done, _) => {
                    setTimeout(done, 10);
                });

                await waitFlush;

                const data = fs.readFile(backupPath);

                const blob = new Blob([data], {
                    type: 'application/octet-stream',
                    ok: true,
                    status: 200
                });

                const headers = new Headers({
                    'content-length': blob.size
                });

                const response = new Response(blob, {
                    headers
                });

                await db.cache.put(cachePath, response);

                console.log("Data cached.");
                fs.unlink(backupPath);
                const exists = fs.analyzePath(backupPath).exists;

                console.log(`${backupPath} exists: ${exists}`);
                return 1;
            }
            else {
                console.log("File not found.");
            }

        } catch (expec) {
            console.log(expec);
        }

        return -1;
    };

    db.getCachedFile = async function (filename) {
        let backupPath = `${filename}`;
        let cachePath = `/data/cache/${backupPath.split('.')[0]}.db`;
        let resp = await db.cache.match(cachePath);
        let blob = await resp.blob();
        let array = await blob.arrayBuffer();
        let uint8array = new Uint8Array(array);
        return uint8array;
    }

    db.restoreJsState = function () {
        db.init = false;
    }
})();