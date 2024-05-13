import config from "../config";
import { Recipe } from "../types/recipe";
import { useQuery } from "@tanstack/react-query";

const useFetchRecipes = () => {
    return useQuery<Recipe[], Error>({
        queryKey: ["recipes"],
        queryFn: async () => {
            const response = await fetch(`${config.baseApiUrl}/recipes`)
            return response.json();
        }
    });
};

const useFetchRecipe = (id: number) => {
    return useQuery<Recipe, Error>({
        queryKey: ["recipe", id],
        queryFn: async () => {
            const response = await fetch(`${config.baseApiUrl}/recipe/${id}`)
            return response.json();
        }
    });
}

export { useFetchRecipe, useFetchRecipes };
