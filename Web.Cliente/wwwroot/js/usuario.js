window.onload = function () {
	listar();
}

/*async*/ function listar() {
//	var dataPersonaSinUsuario = await fetchGet("Persona/listarPersonasSinUsuario", "json", null, true)
//	var dataTipoUsuario = await fetchGet("TipoUsuario/listarTipoUsuario","json",null,true)
	pintar({
		url: "Usuario/listarUsuarios",
		cabeceras: ["Foto","Usuario","Persona","Tipo Usuario"],
		propiedades: ["fotopersona","nombreusuario", "nombrepersona", "nombretipousuario"],
		propiedadId: "iidusuario",
		columnaimg: ["fotopersona"],
//		titlePopup: "Usuario",
//		editar: true,
//		eliminar:true,
//		popup: true,
//		urleliminar: "Usuario/eliminarUsuario",
//		parametroeliminar: "id",
//		callbackeliminar: async function () {
//			var dataPersonaSinUsuario = await fetchGet("Persona/listarPersonasSinUsuario", "json", null, true)
//			llenarCombo(dataPersonaSinUsuario, "cboPersonaFormulario", "iidpersona", "nombrecompleto",
//				"-----Seleccione-----", "0")
//		},
//		callbackrecuperar: function () { 
//			document.getElementsByClassName("ocultar")[0].style.display="none"
//		},
//		callbacknuevo: function () {
//			document.getElementsByClassName("ocultar")[0].style.display = "block"
//		}
//	}, {

//		url: "Usuario/buscarUsuarios",
//		formulario: [

//			[
//				{
//					class: "col-md-6",
//					label: "Nombre Usuario",
//					type: "text",
//					name: "nombreusuario"
//				},
//				{
//					class: "col-md-6",
//					label: "Tipo Usuario",
//					type: "combobox",
//					name: "iidtipousuario",
//					data: dataTipoUsuario,
//					propiedadmostrar: "nombretipousuario",
//					valuemostrar: "iidtipousuario",
//					id:"cboTipoUsuarioBusqueda"
//				}

//			]

//		]

//	},
//		{
//			urlguardar: "Usuario/guardarUsuario",
//			urlrecuperar: "Usuario/recuperarUsuario",
//			parametrorecuperar:"id",
//			callbackGuardar: async function () {
//				var dataPersonaSinUsuario = await fetchGet("Persona/listarPersonasSinUsuario", "json", null, true)
//				llenarCombo(dataPersonaSinUsuario, "cboPersonaFormulario", "iidpersona", "nombrecompleto",
//				 "-----Seleccione-----","0")
//			},
//			type:"popup",
//			formulario: [

//				[
//					{
//						class: "col-md-6",
//						label: "Id Usuario",
//						readonly:true,
//						type: "text",
//						name: "iidusuario"
//					},
//					{
//						class: "col-md-6 ocultar",
//						label: "Persona",
//						type: "combobox",
//						name: "iidpersona",
//						data: dataPersonaSinUsuario,
//						propiedadmostrar: "nombrecompleto",
//						valuemostrar: "iidpersona",
//						id: "cboPersonaFormulario"
//					},
//					{
//						class: "col-md-6",
//						label: "Nombre Usuario",
//						type: "text",
//						name: "nombreusuario"
//					},
//					{
//						class: "col-md-6",
//						label: "Tipo Usuario",
//						type: "combobox",
//						name: "iidtipousuario",
//						data: dataTipoUsuario,
//						propiedadmostrar: "nombretipousuario",
//						valuemostrar: "iidtipousuario",
//						id: "cboTipoUsuarioFormulario"
//					}

//				]

//			]

		}

	)
}
    
