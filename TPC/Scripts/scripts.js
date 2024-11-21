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



function validarTamanoYVistaPrevia(input) {
    const maxFileSize = 2 * 1024 * 1024; // 2 MB
    const archivo = input.files[0];
    const lblError = document.getElementById('<%= lblError.ClientID %>');
    const imgPreview = document.getElementById("imgPreview");

    if (archivo) {
        // Validar tamaño del archivo
        if (archivo.size > maxFileSize) {
            lblError.innerText = "El tamaño del archivo no debe superar los 2 MB.";
            input.value = ""; // Limpiar el campo de archivo
            imgPreview.classList.add("d-none"); // Ocultar la vista previa
        } else {
            lblError.innerText = ""; // Limpiar cualquier mensaje de error
            imgPreview.classList.remove("d-none"); // Mostrar la vista previa

            // Mostrar la vista previa de la imagen
            imgPreview.src = URL.createObjectURL(archivo);
        }
    }
}