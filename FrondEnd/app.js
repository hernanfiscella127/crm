const apiBaseUrl = 'https://localhost:7251/api/v1';
let currentOffset = 0;
const pageSize = 5; // Mostrar 5 proyectos por página
let totalProjects = 0; // Almacenar el total de proyectos

document.addEventListener('DOMContentLoaded', function () {
    const projectForm = document.getElementById('create-project-form');
    const taskForm = document.getElementById('create-task-form');
    const editTaskForm = document.getElementById('edit-task-form');
    const searchForm = document.getElementById('search-project-form');
    const interactionForm = document.getElementById('register-interaction-form');

    // Crear Proyecto
    projectForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const projectData = {
            name: document.getElementById('name').value,
            startDate: document.getElementById('startDate').value,
            endDate: document.getElementById('endDate').value,
            client: parseInt(document.getElementById('clientId').value, 10),
            campaignType: parseInt(document.getElementById('campaignTypeId').value, 10),
        };

        createProject(projectData);
    });

    function createProject(projectData) {
        const message = document.getElementById('project-message');
        message.textContent = '';

        fetch(`${apiBaseUrl}/Project`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(projectData),
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
            .catch((error) => {
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
                'Content-Type': 'application/json',
            },
        })
            .then((response) => response.json())
            .then((data) => {
                totalProjects = data.length; // Guardar el total de proyectos
                const projectTableBody = document.getElementById('projects-tbody');
                projectTableBody.innerHTML = ''; // Limpiar tabla existente

                displayProjectsPage(data);
                createPaginationControls();
            })
            .catch((error) => {
                console.error('Error al cargar los proyectos:', error);
            });
    }

    function displayProjectsPage(data) {
        const projectTableBody = document.getElementById('projects-tbody');
        projectTableBody.innerHTML = '';
        const start = currentOffset;
        const end = start + pageSize;

        data.slice(start, end).forEach((project) => {
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
        paginationControls.innerHTML = '';

        if (currentOffset > 0) {
            const prevButton = document.createElement('button');
            prevButton.textContent = 'Anterior';
            prevButton.addEventListener('click', () => {
                currentOffset -= pageSize;
                loadProjects();
            });
            paginationControls.appendChild(prevButton);
        }

        if (currentOffset + pageSize < totalProjects) {
            const nextButton = document.createElement('button');
            nextButton.textContent = 'Siguiente';
            nextButton.addEventListener('click', () => {
                currentOffset += pageSize;
                loadProjects();
            });
            paginationControls.appendChild(nextButton);
        }
    }

    // Búsqueda de proyectos con paginación
    searchForm.addEventListener('submit', function (event) {
        event.preventDefault();

        const searchData = {
            name: document.getElementById('search-name').value,
            client: document.getElementById('search-clientId').value,
            campaign: document.getElementById('search-campaignTypeId').value,
            offset: currentOffset,
            size: pageSize,
        };

        searchProjects(searchData);
    });

    function searchProjects(searchData) {
        const params = new URLSearchParams();

        if (searchData.name) params.append('name', searchData.name);
        if (searchData.client) params.append('client', searchData.client);
        if (searchData.campaign) params.append('campaign', searchData.campaign);
        params.append('offset', searchData.offset);
        params.append('size', searchData.size);

        fetch(`${apiBaseUrl}/Project?${params.toString()}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        })
            .then((response) => response.json())
            .then((data) => {
                displaySearchResults(data);
            })
            .catch((error) => {
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
        table.classList.add('styled-table');

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
        results.forEach((project) => {
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

    // Cargar la lista de proyectos al iniciar
    loadProjects();
});
