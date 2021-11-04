import React from 'react'
import { useState } from 'react';
import { Step } from './Step';

export const Task = (props) => {
    const [steps, setSteps] = useState([]);
    const [showSteps, setShowSteps] = useState(false);

    const getStepsOfTask = () => {
        fetch(`https://localhost:5001/Hierarchy/task/steps/${props.id}`)
        .then(response => response.json())
        .then(fetchtedStepts => setSteps(fetchtedStepts));
    };

    const showStepsOfTask = () => {
        setShowSteps(!showSteps);
        getStepsOfTask();
    };

    return (
        <div> 
            <ul>
                <li onClick={showStepsOfTask}>{props.name}</li>
                {
                    showSteps && steps.map(step => {
                        return <Step key={step.id} name={step.name} id={step.id}/>
                    })
                }
            </ul>
        </div>
    )
}
