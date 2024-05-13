import { useParams } from "react-router-dom";
import { useFetchRecipe } from "../hooks/RecipeHooks";
import ApiStatus from "../apiStatus";
import defaultPhoto from "./defaultPhoto";

const RecipeDetail = () => {
    const { id } = useParams(); 
    if(!id) 
    {
      throw Error("Recipe id not found");  
    }

    const recipeId = parseInt(id);

    const {data, status, isSuccess} = useFetchRecipe(recipeId);

    if(!isSuccess)
    {
        return <ApiStatus status={status} />
    }
    if(!data)
    {
        return <div>Recipe Not Found</div>
    }
    return (
        <div className="row">
            <div className="col-6">
                <img className="img-fluid" src={data.image  ? data.image : defaultPhoto} alt={data.title} />  
            </div>
            <div className="col-6">
                {data.title}
            </div>
            <div className="col-6">
                {data.description}
            </div>
            
        </div>
    )
}

export default RecipeDetail;