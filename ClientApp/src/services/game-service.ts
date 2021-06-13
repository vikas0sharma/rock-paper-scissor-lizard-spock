import RequestConfig from "../global";
import Axios from "axios";
import { Player } from "../models/player";

export const createGame = async () => {
	const response = await Axios.post('/game', RequestConfig);
	return response.data as string;
}

export const addPlayer = async (gameId: string, playerName: string) => {
	const response = await Axios.post(`/game/${gameId}/players`, {
		name: playerName
	}, RequestConfig);

	return response.data as string;
}

export const getPlayers = async (gameId: string): Promise<Player[]> => {
	const response = await Axios.get(`/game/${gameId}/players`, RequestConfig);
	return response.data as Player[];
}

export const updatePlayerChoice = async (gameId: string, player: Player) => {
	const response = await Axios.put(`/game/${gameId}/players`, player, RequestConfig);
	return response.data;
}
