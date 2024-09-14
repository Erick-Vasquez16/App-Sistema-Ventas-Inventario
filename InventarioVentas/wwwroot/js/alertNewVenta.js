function calcularTotal() {
    let precio = parseFloat(document.getElementById('precioProducto').value) || 0;
    let cantidad = parseFloat(document.getElementById('cantidadProducto').value) || 0;
    let total = precio * cantidad;
    document.getElementById('totalProducto').value = total.toFixed(2);
}

function mostrarAlertaGuardar() {
    Swal.fire({
        position: "center",
        icon: "success",
        title: "¡Tu venta ha sido guardada!",
        showConfirmButton: false,
        timer: 1500
    }).then(() => {
        document.getElementById("nuevaVentaForm").submit();
    });
}

function confirmarVolver(event) {
    event.preventDefault();
    Swal.fire({
        title: "¿Está seguro?",
        text: "¡Se perderán los datos ingresados!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sí, regresar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '@Url.Action("Lista", "Venta")';
        }
    });
}