import { Recipe } from "../types/recipe";
import { useFetchRecipes } from "../hooks/RecipeHooks";
import ApiStatus from "../apiStatus";
import { useNavigate } from "react-router-dom";

const RecipeList = () => {
    const nav = useNavigate();
    const { data, status, isSuccess } = useFetchRecipes();

        if(!isSuccess)
            return <ApiStatus status={status} />

    return (
        <div>
            <div className="row mb-2">
                <h5 className="themeFontColor text-center">
                    Recipes Available
                </h5>
            </div>
            <table className="table table-hover">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {data && data.map((r: Recipe) => (
                        <tr key={r.id} onClick={() => nav(`/recipe/${r.id}`)}>
                            <td>{r.title}</td>
                            <td>{r.description}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default RecipeList;