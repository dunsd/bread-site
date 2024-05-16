import { Recipe } from "../types/recipe";
import defaultPhoto from "./defaultPhoto";

interface RecipeCardProps {
    recipe: Recipe
}

const RecipeCard: React.FC<RecipeCardProps> = ({ recipe }) => {
    return (
        <div className="card mb-3">
            <div className="card-header">
                <h2>
                   {recipe.title} 
                </h2>
            </div>
            <div className="card-body">
                {recipe.description}
                <img className="img-fluid" src={ recipe.image ? recipe.image : defaultPhoto} alt={recipe.title} />
            </div>
            
        </div>
    )
}
export default RecipeCard;