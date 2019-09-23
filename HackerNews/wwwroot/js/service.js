var service = service || {
    //https://developer.mozilla.org/en-US/docs/Web/API/XMLHttpRequest/Using_XMLHttpRequest
    //Wrapper function for calls 
    request: function(verb, url) {
        return new Promise(function(resolve, reject){
            let xhr = new XMLHttpRequest();

            xhr.onload = resolve;
            xhr.onerror = reject;

            xhr.open(verb, url);
            xhr.send();
        });
    },
}