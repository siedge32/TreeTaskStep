import axios from 'axios';

axios.defaults.baseURL = 'http://localhost:5000/api';

// store request in a constant
const reponseBody = (response) => response.data;

const requests = {
    get: (url) => axios.get(url).then(reponseBody),
}

export const TaskStepsRequest = {
    tasks: () => requests.get('/Hierarchy'),
    stepsOfTask: (id) => requests.get(`/Hierarchy/task/steps/${id}`),
    subSteptsOfStep: (id) => requests.get(`/Hierarchy/step/substep/${id}`)
}
