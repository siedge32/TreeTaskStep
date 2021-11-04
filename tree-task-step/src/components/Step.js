import React from 'react'
import { useState } from 'react';

export const Step = (props) => {
    const [subSteps, setSubSteps] = useState([]);
    const [showSubSteps, setShowSubSteps] = useState(false);

    const getStepsOfTask = () => {
        fetch(`https://localhost:5001/Hierarchy/step/substep/${props.id}`)
        .then(response => response.json())
        .then(fetchtedStepts => setSubSteps(fetchtedStepts));
    };

    const showSubStepsOfStep = () => {
        setShowSubSteps(!showSubSteps);
        getStepsOfTask();
    };

    return (
        <div>
            <ul>
                <li onClick={showSubStepsOfStep}>{props.name}</li>
                {
                    showSubSteps && subSteps.map(subStep => {
                        return <Step key={subStep.id} name={subStep.name} id={subStep.id}/>
                    })
                }
            </ul>
        </div>
    )
}
