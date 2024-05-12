import { NextRequest, NextResponse } from "next/server";
import prisma from "@/lib/prisma";
import { Prisma } from "@prisma/client";

type dat = {
  ques: QueData[];
};
export async function POST(req: Request) {
  const que: dat = await req.json();
  let ret;

  for (let i = 0; i < que.ques.length; i++) {
    try {
      ret = await prisma.questionData.create({ data: que.ques[i] });
    } catch (e) {
      return NextResponse.json({ status: "fail", e });
    }
  }

  return NextResponse.json({ msg: "Done", Inserted: que.ques.length });
}
