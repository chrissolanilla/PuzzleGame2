var ImageUploaderPlugin = {
  ImageUploaderCaptureClick: function () {
    var existingInput = document.getElementById('ImageUploaderInput');
    if (existingInput) {
      existingInput.remove(); // ensure a fresh input each time
    }

    var fileInput = document.createElement('input');
    fileInput.setAttribute('type', 'file');
    fileInput.setAttribute('id', 'ImageUploaderInput');
    fileInput.style.display = 'none';

    fileInput.onchange = function (event) {
      if (event.target.files.length > 0) {
        SendMessage('Canvas', 'FileSelected', URL.createObjectURL(event.target.files[0]));
      }
      fileInput.remove(); // optional: clean up after
    };

    document.body.appendChild(fileInput);
    fileInput.click();
  }
};

mergeInto(LibraryManager.library, ImageUploaderPlugin);

