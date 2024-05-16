import { Recipe } from "../types/recipe";
import { useFetchRecipes } from "../hooks/RecipeHooks";
import ApiStatus from "../apiStatus";
import { useNavigate } from "react-router-dom";
import RecipeCard from "./RecipeCard";


const RecipeList = () => {
    const nav = useNavigate();
    const { data, status, isSuccess } = useFetchRecipes();

        if(!isSuccess)
            return <ApiStatus status={status} />

    return (
        <div>            
            {data && data.map((r: Recipe, index: number) => (
                <RecipeCard key={index} recipe={r} />
            ))}            
        </div>
    )
}

export default RecipeList;