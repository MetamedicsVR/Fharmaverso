mergeInto(LibraryManager.library, {
    IsPortrait: function() {
        return window.innerHeight > window.innerWidth;
    }
});