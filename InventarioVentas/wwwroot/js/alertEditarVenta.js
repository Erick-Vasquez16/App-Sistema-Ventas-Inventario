function calcularTotal() {
    // Obtener los valores de precio y cantidad
    let precio = parseFloat(document.getElementById('precioProducto').value) || 0;
    let cantidad = parseFloat(document.getElementById('cantidadProducto').value) || 0;

    // Calcular el total
    let total = precio * cantidad;

    // Asignar el total al campo de total
    document.getElementById('totalProducto').value = total.toFixed(2);
}

function mostrarAlertaGuardar() {
    Swal.fire({
        position: "center",
        icon: "success",
        title: "¡Tu venta ha sido editada!",
        showConfirmButton: false,
        timer: 1500
    }).then(() => {
        // Enviar el formulario después de la alerta
        document.getElementById("editarVentaForm").submit(); // Envía el formulario manualmente
    });
}

function confirmarVolver(event) {
    event.preventDefault(); // Prevenir la redirección inmediata

    // Mostrar la alerta de confirmación
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
            // Si el usuario confirma, redirigir a la página de lista de ventas
            window.location.href = '@Url.Action("Lista", "Venta")';
        }
    });
}