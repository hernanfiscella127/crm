const apiBaseUrl = 'https://localhost:7251/api/v1';
let currentOffset = 0;
const pageSize = 5; // Mostrar 5 proyectos por página
let totalProjects = 0; // Almacenar el total de proyectos

document.addEventListener('DOMContentLoaded', function () {
    const projectForm = document.getElementById('create-project-form'); 
    const taskForm = document.getElementById('create-task-form'); 
    const editTaskForm = document.getElementById('edit-task-form');
    const searchForm = document.getElementById('search-project-form'); 
    const interactionForm = document.getElementById('register-interaction-form'); // Formulario de interacción

    // Validar fechas
    function validateDates(startDate, endDate) {
        const start = new Date(startDate);
        const end = new Date(endDate);
        const now = new Date();

        if (start < now) {
            return "La fecha de inicio debe ser igual o posterior a la fecha actual.";
        }

        if (end <= start) {
            return "La fecha de fin debe ser posterior a la fecha de inicio.";
        }

        return null; // No hay errores
    }

    // Crear Proyecto
    projectForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const startDate = document.getElementById('startDate').value;
        const endDate = document.getElementById('endDate').value;

        // Validar las fechas antes de enviar
        const validationError = validateDates(startDate, endDate);
        if (validationError) {
            const message = document.getElementById('project-message');
            message.textContent = `Error al crear el proyecto: ${validationError}`;
            message.style.color = 'red';
            return;
        }

        const projectData = {
            name: document.getElementById('name').value,
            start: new Date(startDate).toISOString(), // Formato completo ISO
            end: new Date(endDate).toISOString(),   // Formato completo ISO
            client: parseInt(document.getElementById('clientId').value, 10),
            campaignType: parseInt(document.getElementById('campaignTypeId').value, 10)
        };

        createProject(projectData);
    });

    function createProject(projectData) {
        const message = document.getElementById('project-message');
        message.textContent = '';

        fetch(`${apiBaseUrl}/Project`, {  
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(projectData)
        })
        .then(async (response) => {
            if (response.ok) {
                message.textContent = 'Proyecto creado exitosamente';
                message.style.color = 'green';

                if (projectForm && typeof projectForm.reset === 'function') {
                    projectForm.reset(); 
                }

                loadProjects(); 
            } else {
                const errorData = await response.json();
                message.textContent = `Error al crear el proyecto: ${errorData.message || 'Error desconocido'}`;
                message.style.color = 'red';
            }
        })
        .catch(error => {
            message.textContent = `Error al crear el proyecto: ${error.message || 'Error de red'}`;
            message.style.color = 'red';
            console.error('Error al crear el proyecto:', error);
        });
    }

    // Actualizar la fecha mínima permitida en el campo de fecha de inicio
    document.getElementById('startDate').addEventListener('input', function () {
        const startDate = this.value;
        const endDateField = document.getElementById('endDate');
        endDateField.min = startDate; // Fecha de fin no puede ser anterior a la fecha de inicio
    });

    // Establecer el mínimo de la fecha actual para la fecha de inicio
    const today = new Date().toISOString().split("T")[0];
    document.getElementById('startDate').min = today;

    // Cargar proyectos desde el servidor y mostrar en la tabla
    function loadProjects() {
        fetch(`${apiBaseUrl}/Project`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => {
            totalProjects = data.length; // Guardar el total de proyectos
            const projectTableBody = document.getElementById('projects-tbody');
            projectTableBody.innerHTML = ''; // Limpiar tabla existente

            // Mostrar la primera página de proyectos
            displayProjectsPage(data);
            createPaginationControls(); // Crear controles de paginación
        })
        .catch(error => {
            console.error('Error al cargar los proyectos:', error);
        });
    }

    function displayProjectsPage(data) {
        const projectTableBody = document.getElementById('projects-tbody');
        projectTableBody.innerHTML = ''; // Limpiar el cuerpo de la tabla
        const start = currentOffset;
        const end = start + pageSize;

        data.slice(start, end).forEach(project => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${project.id}</td>
                <td>${project.name}</td>
                <td>${new Date(project.start).toLocaleDateString()}</td>
                <td>${new Date(project.end).toLocaleDateString()}</td>
                <td>${project.client.name}</td>
                <td>${project.campaignType.name}</td>
            `;
            projectTableBody.appendChild(row);
        });
    }

    function createPaginationControls() {
        const paginationControls = document.getElementById('project-pagination-controls');
        paginationControls.innerHTML = ''; // Limpiar controles previos

        // Botón de página anterior
        if (currentOffset > 0) {
            const prevButton = document.createElement('button');
            prevButton.textContent = 'Anterior';
            prevButton.addEventListener('click', () => {
                currentOffset -= pageSize;
                loadProjects(); // Cargar la página anterior
            });
            paginationControls.appendChild(prevButton);
        }

        // Botón de página siguiente
        if ((currentOffset + pageSize) < totalProjects) {
            const nextButton = document.createElement('button');
            nextButton.textContent = 'Siguiente';
            nextButton.addEventListener('click', () => {
                currentOffset += pageSize;
                loadProjects(); // Cargar la siguiente página
            });
            paginationControls.appendChild(nextButton);
        }
    }

    // Cargar la lista de proyectos al iniciar
    loadProjects();
});
