//const inputFile = document.getElementById('inputFile');
//const previewContainer = document.getElementById('imagePreview');
//const previewImage = previewContainer.querySelector('.image-preview__image');
//const previewDefaultText = previewContainer.querySelector('.image-preview__text');

//inputFile.addEventListener("change", function () {
//	const file = this.files[0];

//	if (file) {
//		const reader = new FileReader();

//		previewDefaultText.style.display = "none";
//		previewImage.style.display = "block";

//		reader.addEventListener("load", function () {
//			previewImage.setAttribute("src", this.result);
//		});
//		reader.readAsDataURL(file);
//	} else {
//		previewDefaultText.style.display = "null";
//		previewImage.style.display = "null";
//		previewImage.setAttribute("src", "");
//	}

//});


//var input = document.querySelector('#file-input');
//input.addEventListener('change', updateImage);

//function updateImage() {
//	var fileObject = this.files[0];
//	var fileReader = new FileReader();
//	fileReader.readAsDataURL(fileObject);
//	fileReader.onload = function () {
//		var result = fileReader.result;
//		var img = document.querySelector('#preview');
//		img.setAttribute('src', result);
//	}
//}