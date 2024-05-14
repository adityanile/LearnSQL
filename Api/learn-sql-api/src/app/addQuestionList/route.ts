import { NextRequest, NextResponse } from "next/server";
import prisma from "@/lib/prisma";
import { Prisma } from "@prisma/client";

type dat = {
  ques: QueData[];
};
export async function POST(req: Request) {
  const que: dat = await req.json();
  let ret;

  try {
    ret = await prisma.questionData.createMany({
      data: que.ques,
    });
  } catch (e) {
    return NextResponse.json({ status: "fail", e });
  }

  return NextResponse.json({ msg: "Done", Inserted: que.ques.length });
}
