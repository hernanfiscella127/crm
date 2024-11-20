const apiBaseUrl = 'https://localhost:7251/api'; 
let currentOffset = 0; 
const pageSize = 5; // Mostrar 5 proyectos por página
let totalProjects = 0; // Almacenar el total de proyectos

document.addEventListener('DOMContentLoaded', function () {
    const projectForm = document.getElementById('create-project-form'); 
    const taskForm = document.getElementById('create-task-form'); 
    const editTaskForm = document.getElementById('edit-task-form');
    const searchForm = document.getElementById('search-project-form'); 
    const interactionForm = document.getElementById('register-interaction-form'); // Formulario de interacción

    // Crear Proyecto
    projectForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const projectData = {
            name: document.getElementById('name').value,
            startDate: document.getElementById('startDate').value,
            endDate: document.getElementById('endDate').value,
            clientId: parseInt(document.getElementById('clientId').value, 10),
            campaignTypeId: parseInt(document.getElementById('campaignTypeId').value, 10)
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
                <td>${new Date(project.startDate).toLocaleDateString()}</td>
                <td>${new Date(project.endDate).toLocaleDateString()}</td>
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

    // Búsqueda de proyectos con paginación
    searchForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const searchData = {
            name: document.getElementById('search-name').value,
            clientId: document.getElementById('search-clientId').value,
            campaignTypeId: document.getElementById('search-campaignTypeId').value,
            offset: currentOffset,
            size: pageSize
        };

        searchProjects(searchData);
    });

    function searchProjects(searchData) {
        const params = new URLSearchParams();

        if (searchData.name) params.append('name', searchData.name);
        if (searchData.clientId) params.append('clientId', searchData.clientId);
        if (searchData.campaignTypeId) params.append('campaignTypeId', searchData.campaignTypeId);
        params.append('offset', searchData.offset);
        params.append('size', searchData.size);

        fetch(`${apiBaseUrl}/Project?${params.toString()}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => {
            displaySearchResults(data);
        })
        .catch(error => {
            console.error('Error al buscar los proyectos:', error);
        });
    }

    function displaySearchResults(results) {
        const searchResults = document.getElementById('search-results');
        searchResults.innerHTML = '';

        if (results.length === 0) {
            searchResults.textContent = 'No se encontraron proyectos.';
            return;
        }

        const table = document.createElement('table');
        table.classList.add('styled-table'); // Clase para los estilos

        const thead = document.createElement('thead');
        thead.innerHTML = `
            <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Fecha de Inicio</th>
                <th>Fecha de Fin</th>
                <th>Cliente</th>
                <th>Tipo de Campaña</th>
            </tr>
        `;
        table.appendChild(thead);

        const tbody = document.createElement('tbody');
        results.forEach(project => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${project.id}</td>
                <td>${project.name}</td>
                <td>${new Date(project.startDate).toLocaleDateString()}</td>
                <td>${new Date(project.endDate).toLocaleDateString()}</td>
                <td>${project.client.name}</td>
                <td>${project.campaignType.name}</td>
            `;
            tbody.appendChild(row);
        });

        table.appendChild(tbody);
        searchResults.appendChild(table);
    }

    // Crear Tarea
    taskForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const projectId = document.getElementById('project-id').value;
        const taskData = {
            name: document.getElementById('task-name').value,
            dueDate: new Date(document.getElementById('task-date').value).toISOString(),
            userId: parseInt(document.getElementById('user-id').value, 10),
            statusId: parseInt(document.getElementById('status-id').value, 10)
        };

        createTask(projectId, taskData);
    });

    function createTask(projectId, taskData) {
        const taskMessage = document.getElementById('task-message');
        taskMessage.textContent = '';

        fetch(`${apiBaseUrl}/Project/${projectId}/tasks`, {  
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(taskData)
        })
        .then(async (response) => {
            if (response.ok) {
                const responseData = await response.json();
                taskMessage.textContent = `Tarea creada con éxito: ID ${responseData.id}, Asignado a: ${responseData.userAssigned.name}`;
                taskMessage.style.color = 'green';

                if (taskForm && typeof taskForm.reset === 'function') {
                    taskForm.reset(); 
                }
            } else {
                const errorData = await response.json();
                taskMessage.textContent = `Error al crear la tarea: ${errorData.message || 'Error desconocido'}`;
                taskMessage.style.color = 'red';
            }
        })
        .catch(error => {
            taskMessage.textContent = `Error al crear la tarea: ${error.message || 'Error de red'}`;
            taskMessage.style.color = 'red';
            console.error('Error al crear la tarea:', error);
        });
    }

    // Editar Tarea
    editTaskForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const taskId = document.getElementById('edit-task-id').value;
        const taskData = {
            name: document.getElementById('edit-task-name').value,
            dueDate: new Date(document.getElementById('edit-task-date').value).toISOString(),
            userId: parseInt(document.getElementById('edit-assigned-user').value, 10),
            statusId: parseInt(document.getElementById('edit-task-status').value, 10)
        };

        updateTask(taskId, taskData);
    });

    function updateTask(id, taskData) {
        const editTaskMessage = document.getElementById('edit-task-message');
        editTaskMessage.textContent = ''; // Limpiar mensaje previo

        fetch(`${apiBaseUrl}/Project/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(taskData)
        })
        .then(async (response) => {
            if (response.ok) {
                const responseData = await response.json();
                editTaskMessage.textContent = `Tarea actualizada con éxito: ID ${responseData.id}, Asignado a: ${responseData.userAssigned.name}`;
                editTaskMessage.style.color = 'green';

                if (editTaskForm && typeof editTaskForm.reset === 'function') {
                    editTaskForm.reset(); 
                }
            } else {
                const errorData = await response.json();
                editTaskMessage.textContent = `Error al actualizar la tarea: ${errorData.message || 'Error desconocido'}`;
                editTaskMessage.style.color = 'red';
                console.error('Detalles del error:', errorData);
            }
        })
        .catch(error => {
            editTaskMessage.textContent = `Error al actualizar la tarea: ${error.message || 'Error de red'}`;
            editTaskMessage.style.color = 'red';
            console.error('Error al actualizar la tarea:', error);
        });
    }

    // Registrar Interacción
    interactionForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const projectId = document.getElementById('interaction-project-id').value;
        const interactionData = {
            notes: document.getElementById('interaction-notes').value,
            date: new Date(document.getElementById('interaction-date').value).toISOString(),
            interactionTypeId: parseInt(document.getElementById('interaction-type').value, 10)
        };

        registerInteraction(projectId, interactionData);
    });

    function registerInteraction(projectId, interactionData) {
        const interactionMessage = document.getElementById('interaction-message');
        interactionMessage.textContent = '';

        fetch(`${apiBaseUrl}/Project/${projectId}/interactions`, {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(interactionData)
        })
        .then(async (response) => {
            if (response.ok) {
                let responseData;
                try {
                    responseData = await response.json();
                } catch (err) {
                    responseData = {}; // Si no hay cuerpo, inicializar como objeto vacío
                }

                interactionMessage.textContent = `Interacción registrada con éxito: ID ${responseData.id || 'Desconocido'}`;
                interactionMessage.style.color = 'green';
                interactionForm.reset();
            } else {
                const errorData = await response.json();
                interactionMessage.textContent = `Error al registrar la interacción: ${errorData.message || 'Error desconocido'}`;
                interactionMessage.style.color = 'red';
            }
        })
        .catch(error => {
            interactionMessage.textContent = `Error al registrar la interacción: ${error.message || 'Error de red'}`;
            interactionMessage.style.color = 'red';
            console.error('Error al registrar la interacción:', error);
        });
    }

    // Cargar la lista de proyectos al iniciar
    loadProjects();
});
