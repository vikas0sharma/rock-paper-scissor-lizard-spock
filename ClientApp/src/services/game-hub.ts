import { HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { Player } from '../models/player';

const setUpSignalRConnection = async (gameId: string,
	onPlayersUpdated: (players: Player[]) => void,
	onWinnerChanged: (player: Player) => void) => {
	debugger;
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

	connection.on("WinnerSelected", (winner: Player) => {
		onWinnerChanged(winner);
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