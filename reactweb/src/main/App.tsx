import './App.css';
import React from 'react';
import Header from './Header';
import RecipeList from '../recipe/RecipeList';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import RecipeDetail from '../recipe/RecipeDetail';

const App = () => {
  return (
    <BrowserRouter>
      <div className='container'>
        <Header subtitle='Recipes' />
        <Routes>
          <Route path='/' element={<RecipeList/>}/>  
          <Route path='/recipe/:id' element={<RecipeDetail/>}/>
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
