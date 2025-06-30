window.onload = function () {
	listar();
}

function listar() {
	pintar({
		url: "Persona/ListarPersonas",
		cabeceras: ["Foto","Nombre Completo", "Fecha Nacimiento", "Correo"],
		propiedades: ["fotocadena","nombrecompleto", "fechanacimientocadena", "correo"],
		propiedadId: "iidpersona",
		columnaimg: ["fotocadena"],
		popup: true,

		editar: true,
		eliminar: true,
		titlePopup: "Persona",
		urleliminar: "Persona/EliminarPersona",
		parametroeliminar:"id"
	}, {
		url: "Persona/FiltrarPersonas",
		formulario: [
			[
				{
					class: "col-md-6",
					label:"Ingrese nombre completo",
					type: "text",
					name:"nombrecompleto",
				}
			]
		]

	},
	{
		type: "popup",
	 	urlrecuperar: "Persona/RecuperarPersona",
		parametrorecuperar: "id",
		urlguardar: "Persona/GuardarPersona",
		formulario: [
			[
				{
					class: "col-md-6 d-none",
					label: "Id Persona",
					type: "text",
					name: "iidpersona"
				},
				{
					class: "col-md-6",
					label: "Nombre",
					type: "text",
					name: "nombre",
					classControl:"ob max-100"
				},
				{
					class: "col-md-6",
					label: "Apellido Paterno",
					type: "text",
					name: "appaterno",
					classControl: "ob max-150"
				},
				{
					class: "col-md-6",
					label: "Apellido Materno",
					type: "text",
					name: "apmaterno",
					classControl: "ob max-100"
				},
				{
					class: "col-md-6",
					label: "Fecha Nacimiento",
					type: "date",
					name: "fechanacimientocadena",
					classControl: "ob"
				},
				{
					class: "col-md-6",
					label: "Correo",
					type: "text",
					name: "correo",
					classControl: "ob max-100"
				},
				{
					class: "col-md-6",
					label: "Sexo",
					type: "radio",
					labels: ["Masculino", "Femenino"],
					values: ["1", "2"],
					ids: ["rbMasculino", "rbFemenino"],
					checked:"rbMasculino",
					name:"iidsexo"
				},
				{
					class: "col-md-6",
					label: "Foto",
					type: "file",
					label: "Suba una foto",
					name: "fotoenviar",
					preview: true,
					imgwidth: 100,
					imgheight: 100,
					namefoto: "fotocadena",
					classControl: "ob"
				}
			]

		]
	}

	)
}