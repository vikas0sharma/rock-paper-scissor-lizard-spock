import { Choice } from "./player";

export interface Result {
	round: number;
	win: string;
	winner: string;
	choices: [{ id: string, value: ScoreResult }];
}

export interface ScoreResult {
	score: number;
	choice: Choice;
}