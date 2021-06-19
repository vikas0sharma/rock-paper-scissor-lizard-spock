import { HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { Player } from '../models/player';
import { Result } from '../models/result';

const setUpSignalRConnection = async (gameId: string,
	onPlayersUpdated: (players: Player[]) => void,
	onWinnerChanged: (result: Result) => void) => {
	const connection = new HubConnectionBuilder()
		.withUrl('/gamehub')
		.withAutomaticReconnect()
		.build();

	connection.on("GameConnected", (msg: string) => {
		console.log(msg);
	});

	connection.on("PlayersUpdated", (players: Player[]) => {
		console.log(players);
		onPlayersUpdated(players);
	});

	connection.on("WinnerSelected", (result: Result) => {
		console.log(result);
		onWinnerChanged(result);
	})

	try {
		await connection.start();
	} catch (error) {
		console.error(error);
	}

	if (connection.state === HubConnectionState.Connected) {
		try {
			await connection.invoke("GameStart", gameId);
		} catch (error) {
			console.error(error);
		}
	}
}

export default setUpSignalRConnection;