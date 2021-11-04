import './App.css';
import {useState, useEffect} from 'react';
import { Task } from './components/Task';

export const App = () => {
  const [tasks, setTasks] = useState([]);

  useEffect(() => {
    fetch('https://localhost:5001/Hierarchy')
        .then(response => response.json())
        .then(tasks => setTasks(tasks));
  }, []);

  return (
    <div className="root">
      {
        tasks.map((task) => {
          return <Task key={task.id} name={task.name} id={task.id}/>
      } )}
    </div>
  )
}
