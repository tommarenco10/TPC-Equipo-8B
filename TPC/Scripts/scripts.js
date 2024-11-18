function previewImage(event) {
    const imgPreview = document.getElementById("imgPreview");
    imgPreview.src = URL.createObjectURL(event.target.files[0]);
    imgPreview.classList.remove("d-none");
}
