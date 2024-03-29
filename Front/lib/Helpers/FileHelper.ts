function blobToByteArray(blob: Blob | undefined) {
    if (!blob) {
        return null;
    }
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onloadend = function() {
           const byteArray = new Uint8Array(reader.result as ArrayBuffer);
            resolve(byteArray);
        }
        reader.onerror = reject;
        reader.readAsArrayBuffer(blob);
    });
}