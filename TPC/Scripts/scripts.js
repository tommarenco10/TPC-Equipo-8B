function previewImage(event) {
    const imgPreview = document.getElementById("imgPreview");
    imgPreview.src = URL.createObjectURL(event.target.files[0]);
    imgPreview.classList.remove("d-none");
}



function validarTamanoArchivo(input) {
    const maxFileSize = 2 * 1024 * 1024;    
    const archivo = input.files[0]; 
    const lblError = document.getElementById('<%= lblError.ClientID %>');

    if (archivo) {
        if (archivo.size > maxFileSize) {
            lblError.innerText = "El tamaño del archivo no debe superar los 2 MB.";
            input.value = "";   
        } else {
            lblError.innerText = "";    
        }
    }
}