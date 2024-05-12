import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";

export async function GET(req: Request) {
  // const ret = await prisma.questionData.findMany({
  //   where: {
  //     level: 1,
  //     tag: "Theor",
  //   },
  // });

  const ret = await prisma.questionData.deleteMany({});

  return NextResponse.json({ msg: "Success", ret });
}
