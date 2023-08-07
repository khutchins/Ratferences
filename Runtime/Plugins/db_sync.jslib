mergeInto(LibraryManager.library, {
    //flush our file changes to IndexedDB
    SyncWebGLFileSystem: function () {
        FS.syncfs(false, function (err) {
           if (err) console.log("syncfs error: " + err);
        });
    }
});