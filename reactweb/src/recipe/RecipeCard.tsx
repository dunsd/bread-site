import { useNavigate } from "react-router-dom";
import { Recipe } from "../types/recipe";
import defaultPhoto from "./defaultPhoto";
import "./recipe.scss";

interface RecipeCardProps {
    recipe: Recipe
}

const RecipeCard: React.FC<RecipeCardProps> = ({ recipe }) => {

    const nav = useNavigate();

    return (
        <div className="card mb-3 recipe-card-header" onClick={() => nav(`/recipe/${recipe.id}`)}>
            <div className="card-header" >                
                   <h3 className="">{recipe.title}</h3>
            </div>
            <div className="card-body">
                {recipe.description}
                <img className="img-fluid" src={ recipe.image ? recipe.image : defaultPhoto} alt={recipe.title} />
            </div>
            
        </div>
    )
}
export default RecipeCard;