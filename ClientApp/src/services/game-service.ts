import RequestConfig from "../global";
import Axios from "axios";

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
