export interface Player {
	id: string,
	name: string,
	choice : Choice,
	score:number;
}

export enum Choice {
	Unknown = 0,
	Rock = 1,
	Paper = 2,
	Scissor = 3,
	Lizard = 4,
	Spock = 5
}