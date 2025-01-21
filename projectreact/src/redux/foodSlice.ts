import { createSlice } from "@reduxjs/toolkit";
import { fetchGetFood } from "./operation";

export interface FoodState {
  food: Array<{
    id: number;
    name: string;
    price: number;
    description: string;
    theme: string;
    image: string;
  }>;
  isLoading: boolean;
  error: string | undefined;
}

const initialState: FoodState = {
  food: [],
  isLoading: true,
  error: undefined,
};

const foodSlice = createSlice({
  name: "food",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchGetFood.pending, (state) => {
        state.isLoading = true;
        state.error = undefined;
      })
      .addCase(fetchGetFood.fulfilled, (state, action) => {
        state.isLoading = false;
        state.food = action.payload;
      })
      .addCase(fetchGetFood.rejected, (state, action) => {
        state.isLoading = false;
        state.error = action.error.message;
      });
  },
});

export default foodSlice.reducer;
